using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
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

        // todo lesson 5-04b Add GoogleLogin action
        [AllowAnonymous]
        public IActionResult GoogleLogin(string returnUrl) {
            string redirectUrl = Url.Action("GoogleResponse", "Users",
                new { ReturnUrl = returnUrl });
            AuthenticationProperties properties
                = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            // A ChallengeResult is an ActionResult that when executed, 
            // challenges the given authentication schemes' handler. 
            // here:  causes ASP.NET Core Identity to respond to an unauthorized error 
            //          by redirecting the user to the Google authentication page 
            // A challenge is basically a way of saying "I don't know who this user is, please verify their identity".

            // todo lesson 5-04c: trigger external login (Google)
            return new ChallengeResult(GoogleDefaults.AuthenticationScheme,
                properties);
        }

        // todo lesson 5-05a: GoogleResponse - handle response from Google authentication service
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse(string returnUrl = "/") {

            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null) {
                return RedirectToAction(nameof(Login));
            }
            // todo lesson 5-05b use: signInManager.ExternalLoginSignInAsync:
            //     Signs in a user via a previously registered third party login, as an asynchronous operation.
            Microsoft.AspNetCore.Identity.SignInResult result
                = await signInManager.ExternalLoginSignInAsync(
                    info.LoginProvider, info.ProviderKey, false);


            if (result.Succeeded) {
                return Redirect(returnUrl);
            } else {
                IdentityUser user = new IdentityUser {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value
                };
                IdentityResult identityResult = await userManager.CreateAsync(user);
                if (identityResult.Succeeded) {
                    identityResult = await userManager.AddLoginAsync(user, info);
                    if (identityResult.Succeeded) {
                        await signInManager.SignInAsync(user, false);
                        return Redirect(returnUrl);
                    }
                }
                return AccessDenied(returnUrl);
            }
        }

    }
}
