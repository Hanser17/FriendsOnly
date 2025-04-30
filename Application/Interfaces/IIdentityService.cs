using Application.DTOs.UserDtos;
using Application.ViewModels.User;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResponse> GetByUsername(Authenticationrequest userName);
        Task UpdateAsync(RegisterRequest vm);
        Task<AuthenticationResponse> AuthenticateAsync(Authenticationrequest dtoRequest);
        Task<string> ConfirmAccount(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPassWordAsync(ForgotPasswordRequest dtoRequest, string origin);
        Task LogOut();
        Task<RegisterResponse> RegisterAsync(RegisterRequest dtoRequest, string origin);
        Task<ResetPasswordResponse> ResetPassWordAsync(ResetPasswordRequest dtoRequest);

        Task<int> GetActiveCustomers();
        Task<int> GetInactiveCustomers();

        Task <List<AuthenticationResponse>> GetAdmins();
        Task<List<AuthenticationResponse>> GetClients();
        Task<List<AuthenticationResponse>> GetActiveUsers();
        Task<List<AuthenticationResponse>> GetInactiveUsers();

        Task<string> ActiveORInActuveUser(string userId, bool state);


    }
}