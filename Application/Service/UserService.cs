using Application.DTOs.UserDtos;
using Application.Interfaces;
using Application.ViewModels.User;
using AutoMapper;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserService(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LogingAsync(LoginUserViewModel vm)
        {
            Authenticationrequest loginRequest = _mapper.Map<Authenticationrequest>(vm);
            AuthenticationResponse userResponce = await _identityService.AuthenticateAsync(loginRequest);


            return userResponce;
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _identityService.RegisterAsync(registerRequest, origin);

        }
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _identityService.ConfirmAccount(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _identityService.ForgotPassWordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _identityService.ResetPassWordAsync(resetRequest);
        }
        public async Task SignOutAsync()
        {   
            await _identityService.LogOut();
        }

        public async Task<AuthenticationResponse> GetByUserName(string userName)
        {
            Authenticationrequest loginRequest = new Authenticationrequest { UserName = userName };

           
            AuthenticationResponse userResponse = await _identityService.GetByUsername(loginRequest);

            return userResponse;
        }

        public async Task UpdateAsync(AuthenticationResponse vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
             await _identityService.UpdateAsync(registerRequest);
        }

        public async Task<int> GetActiveCustomers()
        {
            return await _identityService.GetActiveCustomers();
        }

        public async Task<int> GetInactiveCustomers()
        {
            return await _identityService.GetInactiveCustomers();
        }

        public async  Task<List<AuthenticationResponse>> GetAdmins()
        {
            return await _identityService.GetAdmins();
        }

        public async Task<List<AuthenticationResponse>> GetClients()
        {
            return await _identityService.GetClients();
        }

        public async Task<List<AuthenticationResponse>> GetActiveUsers()
        {
            return await _identityService.GetActiveUsers();
           
        }

        public async Task<List<AuthenticationResponse>> GetInactiveUsers()
        {
            return await _identityService.GetInactiveUsers();
        }

        public async Task<string> ActiveORInActuveUser(string userId, bool state)
        {
           return await _identityService.ActiveORInActuveUser(userId, state);
        }
    }
}
