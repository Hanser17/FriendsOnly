

using Application.DTOs.Email;
using Application.DTOs.UserDtos;
using Application.Enums;
using Application.Interfaces;
using AutoMapper;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<User> userManager, SignInManager<User> singInManager, IEmailService emailService, IMapper mapper)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(Authenticationrequest dtoRequest)
        {
            AuthenticationResponse dtoResponse = new();


            var user = await _userManager.FindByNameAsync(dtoRequest.UserName);
            if (user == null)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ("No User found with the User Name provided");
                return dtoResponse;
            }

            var result = await _singInManager.PasswordSignInAsync(user.UserName ?? "", dtoRequest.PassWord, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ("No User found with the credentials provided");
                return dtoResponse;
            }
            if (!user.EmailConfirmed)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ("Account not active yet");
                return dtoResponse;
            }
            dtoResponse.Id = user.Id;
            dtoResponse.UserName = user.UserName;
            var Roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            dtoResponse.Roles = [.. Roles];
            dtoResponse.Email = user.Email;
            dtoResponse.IsVerified = user.EmailConfirmed;

            return dtoResponse;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest dtoRequest, string origin)
        {
            RegisterResponse dtoResponse = new()
            {
                HasError = false
            };

            var userName = await _userManager.FindByNameAsync(dtoRequest.UserName);
            if (userName != null)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"User name '{dtoRequest.UserName} already taken'");
                return dtoResponse;
            }
            var user = new User
            {
                Email = dtoRequest.Email,
                FirstName = dtoRequest.FirstName,
                LastName = dtoRequest.LastName,
                PhoneNumber = dtoRequest.Phone,
                UserName = dtoRequest.UserName,
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dtoRequest.Password);

            if (!result.Succeeded)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ("Error registering the user");
                return dtoResponse;
            }
            else
            {
                await _userManager.AddToRoleAsync(user, dtoRequest.Roles.ToString());
               var VerificationURL = await ActivateAccount(user, origin);
              /*  await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please Activate your account by visiting the following URL {VerificationURL}",
                    Subject = "Account Activation"
                });*/
            }

            return dtoResponse;

        }
        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No account found with user Id provided";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account Activated";
            }
            else
            {
                return $"Error ocurred while confirming";
            }

        }
        public async Task<ForgotPasswordResponse> ForgotPassWordAsync(ForgotPasswordRequest dtoRequest, string origin)
        {
            ForgotPasswordResponse dtoResponse = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(dtoRequest.UserName);
            if (user == null)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"No User Account Found");
                return dtoResponse;
            }
            var VerificationURL = await ResetPasswordAccount(user, origin);
            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account password by visiting the following URL {VerificationURL}",
                Subject = "Reset Password"
            });


            return dtoResponse;

        }
        public async Task<ResetPasswordResponse> ResetPassWordAsync(ResetPasswordRequest dtoRequest)
        {
            ResetPasswordResponse dtoResponse = new()
            {
                HasError = false
            };
            var user = await _userManager.FindByNameAsync(dtoRequest.UserName);
            if (user == null)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"No User Account Found");
                return dtoResponse;
            }
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dtoRequest.Token));
            var result = await _userManager.ResetPasswordAsync(user, token, dtoRequest.Password);
            if (!result.Succeeded)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"Error ocurred while changing password");
                return dtoResponse;

            }

            return dtoResponse;
        }

        public async Task LogOut()
        {
            await _singInManager.SignOutAsync();
        }



        private async Task<string> ActivateAccount(User user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ActivateAccount";
            var url = new Uri(string.Concat($"{origin}/", route));
            var VerificationURL = QueryHelpers.AddQueryString(url.ToString(), "userId", user.Id);
            VerificationURL = QueryHelpers.AddQueryString(VerificationURL, "token", code);
            return VerificationURL;
        }

        private async Task<string> ResetPasswordAccount(User user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var VerificationURL = QueryHelpers.AddQueryString(url.ToString(), "token", code);
            return VerificationURL;
        }

        public async Task<AuthenticationResponse> GetByUsername(Authenticationrequest userName)
        {
            var user = await _userManager.FindByNameAsync(userName.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return new AuthenticationResponse
                {
                    HasError = true,
                    Error = "No User Account Found"
                };
            }

            var dtoResponse = new AuthenticationResponse
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Phone = user.PhoneNumber,
                UserName = user.UserName,
                Email = user.Email,
                IsVerified = user.EmailConfirmed,
                Roles = roles.ToList(),
                HasError = false
            };

            return dtoResponse;
        }

        public async Task UpdateAsync(RegisterRequest dtoRequest)
        {
            RegisterResponse dtoResponse = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByNameAsync(dtoRequest.UserName);
            if (user == null)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"User name Not Found");
                
            }

            user.Email = dtoRequest.Email;
            user.FirstName = dtoRequest.FirstName;
            user.LastName = dtoRequest.LastName;
            user.PhoneNumber = dtoRequest.Phone;
            user.UserName = dtoRequest.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                dtoResponse.HasError = true;
                dtoResponse.Error = ($"Error updating user");
               
            }
           
        }

        public async Task<int> GetActiveCustomers()
        {
            return await _userManager.Users.Where(u => u.IsActive == true).CountAsync();
        }

        public async Task<int> GetInactiveCustomers()
        {
            return await _userManager.Users.Where(u => u.IsActive == false ).CountAsync();
        }

        public async Task<List<AuthenticationResponse>> GetAdmins()
        {
            var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin.ToString());

            var adminsList = admins.Select(admin => new AuthenticationResponse
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                UserName = admin.UserName,
                Phone = admin.PhoneNumber
            }).ToList();
            return (adminsList);
        }

        public async Task<List<AuthenticationResponse>> GetClients()
        {
            var client = await _userManager.GetUsersInRoleAsync(Roles.Client.ToString());
            var clientsList = client.Select(client => new AuthenticationResponse
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                UserName = client.UserName,
                Phone = client.PhoneNumber
            }).ToList();
            return (clientsList);
        }

        public async Task<List<AuthenticationResponse>> GetActiveUsers()
        {
            var users = await _userManager.Users.Where(u => u.IsActive == true ).ToListAsync();
            var UsersActive = users.Select(users => new AuthenticationResponse
            {   
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                UserName = users.UserName,
                Phone = users.PhoneNumber
            }).ToList();
            return (UsersActive);
        }

        public async Task<List<AuthenticationResponse>> GetInactiveUsers()
        {
            var Users = await _userManager.Users.Where(u => u.IsActive == false ).ToListAsync();
            var UsersInactive = Users.Select(Users => new AuthenticationResponse
            {   
                Id = Users.Id,
                FirstName = Users.FirstName,
                LastName = Users.LastName,
                Email = Users.Email,
                UserName = Users.UserName,
                Phone = Users.PhoneNumber
            }).ToList();
            return (UsersInactive);
        }

        public async Task<string> ActiveORInActuveUser(string userId, bool state)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return "User not found";
            }
            user.IsActive = state;
            await _userManager.UpdateAsync(user);

            return "User stated changed";
        }
    }
}
