using FriendsOnlyWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FriendsOnlyWeb.Middlewares
{
    public class LoginAuthorized : IAsyncActionFilter
    {
        private readonly ValidateSassion _userSession;

        public LoginAuthorized(ValidateSassion userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_userSession.HasUser())
            {
                if (context.Controller is Controller controller) 
                {
                    context.Result = controller.RedirectToAction("Index", "User"); 
                }
                return; 
            }

            await next(); 

        }
    }
   
}
