using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSportsClub.Models;
using MvcSportsClub.ViewModel;


namespace MvcSportsClub.Controllers {
    public class UsersController : Controller {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersController(UserManager<IdentityUser> userManager
                              , SignInManager<IdentityUser> signInManager
                              , RoleManager<IdentityRole> roleManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        // todo lesson 4-19b - acces denied
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl) {
            return new ObjectResult("Foei " + User.Identity.Name + "! Daar mag jij niet komen!");
        // betere mogelijkheid:
        //    //return RedirectToAction(returnUrl);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model) {
            if (ModelState.IsValid) {
                // todo lesson 4-08: maak controller-action voor [HhtpPost] Register.
                IdentityUser user = new IdentityUser {
                  UserName = model.Name
                , Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login() {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            // todo lesson 4-14: maak controller-action voor [HttpPost] Login.
            if (ModelState.IsValid) {
                Microsoft.AspNetCore.Identity.SignInResult result
                    = await signInManager.PasswordSignInAsync(
                            model.Name, 
                            model.Password, 
                            isPersistent:false, 
                            lockoutOnFailure:false);

                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login failed.");
            }

            return View(model);
        }

    }
}
