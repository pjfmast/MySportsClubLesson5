using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MvcSportsClub.Data {
    public static class UserAndRoleDataInitializer {
        
        public static void SeedRoles(RoleManager<IdentityRole> roleManager) {
            if (!roleManager.RoleExistsAsync("Admin").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Desk").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Desk";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Member").Result) {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedUsers(UserManager<IdentityUser> userManager) {
            if (userManager.FindByEmailAsync("admin@avd.nl").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin";
                user.Email = "admin@avd.nl";

                IdentityResult result = userManager.CreateAsync(user, "Admin1234").Result;

                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "Desk").Wait();
                }
            }

            if (userManager.FindByEmailAsync("Henk").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "Henk";
                user.Email = "henk@avd.nl";

                IdentityResult result = userManager.CreateAsync(user, "Henkisok").Result;

                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            if (userManager.FindByEmailAsync("Anne").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "Anne";
                user.Email = "anne@avd.nl";
              
                IdentityResult result = userManager.CreateAsync(user, "Anne1234").Result;

                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user, "Desk").Wait();
                }
            }

            if (userManager.FindByEmailAsync("Piet").Result == null) {
                IdentityUser user = new IdentityUser();
                user.UserName = "Piet";
                user.Email = "piet@avd.nl";

                IdentityResult result = userManager.CreateAsync(user, "Piet1234").Result;

                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
        }


    }
}
