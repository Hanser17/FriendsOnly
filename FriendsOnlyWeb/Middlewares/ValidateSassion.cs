using Application.DTOs.UserDtos;
using Application.Helpers;

namespace FriendsOnlyWeb.Middlewares
{
    public class ValidateSassion
    {
        private readonly IHttpContextAccessor _httpContextAccessor ;

        public ValidateSassion(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse AuthenticationResponse = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");

            return AuthenticationResponse != null;
        }
    }
}
