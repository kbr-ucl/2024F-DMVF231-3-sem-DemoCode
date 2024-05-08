using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DomainCentricDemo.Web.Data.DataInitializer
{
    public static class UserAndRoleDataInitializer
    {
        public static async Task SeedDataAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("author@ucl.dk") == null)
            {
                var user = new IdentityUser();
                user.UserName = "author@ucl.dk";
                user.Email = "author@ucl.dk";
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, "test1234");

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("author", ""));
                }
            }


            if (await userManager.FindByEmailAsync("kbr@ucl.dk") == null)
            {
                var user = new IdentityUser();
                user.UserName = "kbr@ucl.dk";
                user.Email = "kbr@ucl.dk";
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, "test1234");

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("author", ""));
                }
            }

            if (userManager.FindByEmailAsync("admin@ucl.dk").Result == null)
            {
                var user = new IdentityUser();
                user.UserName = "admin@ucl.dk";
                user.Email = "admin@ucl.dk";
                user.EmailConfirmed = true;


                var result = await userManager.CreateAsync(user, "test1234");

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("admin", ""));
                }
            }

            if (userManager.FindByEmailAsync("guest@ucl.dk").Result == null)
            {
                var user = new IdentityUser();
                user.UserName = "guest@ucl.dk";
                user.Email = "guest@ucl.dk";
                user.EmailConfirmed = true;


                var result = await userManager.CreateAsync(user, "test1234");

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("guest", ""));
                }
            }
        }

        //private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        //{
        //    if (!await roleManager.RoleExistsAsync("User"))
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "User";
        //        var roleResult = roleManager.CreateAsync(role).Result;
        //    }


        //    if (!roleManager.RoleExistsAsync("Admin").Result)
        //    {
        //        var role = new IdentityRole();
        //        role.Name = "Admin";
        //        var roleResult = roleManager.CreateAsync(role).Result;
        //    }
        //}
    }
}