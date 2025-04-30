using Application.Enums;

using Microsoft.AspNetCore.Identity;


namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync( RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Client.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            
        }
    }
}
