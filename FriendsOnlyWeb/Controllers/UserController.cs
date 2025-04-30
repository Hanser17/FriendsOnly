using Application.DTOs.SavingsAccount;
using Application.DTOs.UserDtos;
using Application.Enums;
using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels.SavingAccount;
using Application.ViewModels.User;
using Application.ViewModels.User.Admin;
using AutoMapper;
using FriendsOnlyWeb.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace NetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingsAccountService _savingsAccount;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse? _UserLogged;


        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper, ISavingsAccountService savingsAccount)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _savingsAccount = savingsAccount;
            _mapper = mapper;
            _UserLogged = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");

        }

        public IActionResult Index()
        {
            return View(new LoginUserViewModel() { UserName = "", Password = "" });
        }


        
        [HttpPost]
        public async Task<IActionResult> Index(LoginUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LogingAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                if (userVm.Roles.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Home", action = "ClientHome" });
                }
               
            }
            else
            {
                vm.HasError = userVm?.HasError ?? true;
                vm.Error = userVm?.Error ?? "";
                return View(vm);
            }
        }

        
        public IActionResult Register()
        {
            return View(new SaveUserViewModel()
            {
                ConfirmPassword = "",
                Email = "",
                FirstName = "",
                LastName = "",
                Password = "",
                Phone = "",
                UserName = ""
              
            });
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"].ToString() ?? string.Empty;

            SaveUserViewModel saveUser = new SaveUserViewModel
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Email = vm.Email,
                Phone = vm.Phone,
                Password = vm.Password,
                ConfirmPassword = vm.ConfirmPassword,
                Roles = vm.Roles,
                
            };

            RegisterResponse response = await _userService.RegisterAsync(saveUser, origin);


            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            var useraAdded = await _userService.GetByUserName(vm.UserName);
            if (useraAdded != null && useraAdded.Id != null)
            {
                if(vm.Roles == Roles.Client)
                {
                    var savingsAccount = new SavingsAccountSaveVM
                    {
                        UserId = useraAdded.Id,
                        Balance = vm.Balance,
                        IsPrimary = true

                    };

                    var savingToAdd = _mapper.Map<SavingsAccountSaveDTO>(savingsAccount);
                    await _savingsAccount.AddAService(savingToAdd);
                }
               
                await _userService.UpdateAsync(useraAdded);
               
            }
            if (_UserLogged != null)
            {
                return RedirectToRoute(new { controller = "User", action = "AdministrationUsers" });

            }
            else
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
                
        }

      
        public async Task<IActionResult> ActivateAccount(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ActivateYourAccount", response);
        }

       
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel() { UserName = "" });
        }

        
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers.Origin.ToString() ?? string.Empty;

            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
       
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token, ConfirmPassword = "", UserName = "", Password = "" });
        }

       
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        public async Task<IActionResult> Edit()
        {
             var userLogged = _UserLogged.UserName;
             var useraAdded = await _userService.GetByUserName(userLogged);
             SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(useraAdded);
             return View("Profile", vm);
        }

        public async Task<IActionResult> Update( string username)
        {
          //  var userLogged = _UserLogged.UserName;
            var useraAdded = await _userService.GetByUserName(username);
            SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(useraAdded);
            return View("Register", vm);
        }

        [ServiceFilter(typeof(LoginAuthorized))]
        [HttpPost]
        public async Task<IActionResult> Update(SaveUserViewModel vm)
        {
            if (vm != null && vm.Id != null)
            {
                var useraAdded = await _userService.GetByUserName(vm.UserName);
                SaveUserViewModel viewModel = _mapper.Map<SaveUserViewModel>(useraAdded);
                AuthenticationResponse update = _mapper.Map<AuthenticationResponse>(vm);
                if (useraAdded.Roles.Contains(Roles.Client.ToString()))
                {
                    var account = await _savingsAccount.GetbyUserId(useraAdded.Id);
                    var savingsAccount = new SavingsAccountSaveVM
                    {
                        UserId = useraAdded.Id,
                        Balance = vm.Balance + account.Balance,
                        IsPrimary = account.IsPrimary,
                    };
                    var savingToAdd = _mapper.Map<SavingsAccountSaveDTO>(savingsAccount);
                    await _savingsAccount.UpdateService(savingToAdd);
                }
                await _userService.UpdateAsync(update);
            }
            return RedirectToRoute(new { controller = "User", action = "AdministrationUsers" });

        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        public async Task<IActionResult> AdministrationUsers()
        {
            var usersAdmins = await _userService.GetAdmins();
            var userClients = await _userService.GetClients();
            var usertActive = await _userService.GetActiveUsers();
            var userInactive = await _userService.GetInactiveUsers();

            var usersLists = new AdministrationUsersVM
            {
                Admins = usersAdmins,
                Clients = userClients,
                ActiveUsers = usertActive,
                InactiveUsers = userInactive
            };
            return View("AdministrationView", usersLists);
        }

        [HttpGet]
        public async Task<IActionResult> ActivateORDisActivateUser( string username, bool state)
        {
             await _userService.ActiveORInActuveUser(username, state);

            var usersAdmins = await _userService.GetAdmins();
            var userClients = await _userService.GetClients();
            var usertActive = await _userService.GetActiveUsers();
            var userInactive = await _userService.GetInactiveUsers();

            var usersLists = new AdministrationUsersVM
            {
                Admins = usersAdmins,
                Clients = userClients,
                ActiveUsers = usertActive,
                InactiveUsers = userInactive
            };
            return View("AdministrationView", usersLists);

           
        }
    }
}

  