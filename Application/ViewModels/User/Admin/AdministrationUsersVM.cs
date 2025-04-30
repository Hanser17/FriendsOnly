

using Application.DTOs.UserDtos;

namespace Application.ViewModels.User.Admin
{
    public class AdministrationUsersVM
    {
        public List<AuthenticationResponse>? Admins { get;set; }
        public List<AuthenticationResponse>? Clients { get; set; }
        public List<AuthenticationResponse>? ActiveUsers { get; set; }
        public List<AuthenticationResponse>? InactiveUsers { get; set; }

    }
}
