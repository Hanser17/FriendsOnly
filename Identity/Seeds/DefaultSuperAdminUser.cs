

using Application.Enums;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            User defaultUser = new()
            {
                UserName = "Hanser17",
                Email = "Hanser17@gmail.com",
                FirstName = "Hanser",
                LastName = "Pimentel",
                IsActive = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByNameAsync(defaultUser.UserName);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    
                }
            }
        }
    }
}
