using Application.DTOs.UserDtos;
using Application.ViewModels.User;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResponse> GetByUserName(string userName);
        Task UpdateAsync(AuthenticationResponse vm);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LogingAsync(LoginUserViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();

        Task<int> GetActiveCustomers();
        Task<int> GetInactiveCustomers();
        Task<List<AuthenticationResponse>> GetAdmins();
        Task<List<AuthenticationResponse>> GetClients();
        Task<List<AuthenticationResponse>> GetActiveUsers();
        Task<List<AuthenticationResponse>> GetInactiveUsers();
        Task<string> ActiveORInActuveUser(string userId, bool state);
    }
}