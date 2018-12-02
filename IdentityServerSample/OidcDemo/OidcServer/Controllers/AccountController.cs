using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OidcServer.ViewModels;
using IdentityServer4.Test;

namespace OidcServer.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //private UserManager<ApplicationUser> _userManager;
        //private SignInManager<ApplicationUser> _signInManager;
        //private IIdentityServerInteractionService _identityServerInteractionService;


        private readonly TestUserStore _users;

        public AccountController(TestUserStore users)
        {
            _users=users;
        }


        //public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IIdentityServerInteractionService identityServerInteractionService)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _identityServerInteractionService = identityServerInteractionService;
        //}

        private IActionResult RedirectToLoacl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            //if (ModelState.IsValid)
            //{
            //    ViewData["ReturnUrl"] = returnUrl;
            //    var identityUser = new ApplicationUser
            //    {
            //        Email = registerViewModel.Email,
            //        UserName = registerViewModel.Email,
            //        NormalizedUserName = registerViewModel.Email,
            //    };

            //    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            //    if (identityResult.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });
            //        return RedirectToLoacl(returnUrl);
            //    }
            //    else
            //    {
            //        AddErrors(identityResult);
            //    }
            //}
            
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var user = _users.FindByUsername(loginViewModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.UserName), "UserName not exists");
                }
                else
                {
                    if (_users.ValidateCredentials(loginViewModel.UserName,loginViewModel.Password))
                    {
                        
                        var props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };

                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                            HttpContext,user.SubjectId,user.Username, props);

                        return RedirectToLoacl(returnUrl);


                    }
                    ModelState.AddModelError(nameof(loginViewModel.Password), "Wrong Password");
                }
            }



            //if (ModelState.IsValid)
            //{
            //    ViewData["ReturnUrl"] = returnUrl;
            //    var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            //    if (user == null)
            //    {
            //        ModelState.AddModelError(nameof(loginViewModel.Email), "Email not exists");
            //    }
            //    else
            //    {
            //        if (await _userManager.CheckPasswordAsync(user, loginViewModel.Password))
            //        {
            //            AuthenticationProperties props = null;
            //            if (loginViewModel.RememberMe)
            //            {
            //                props = new AuthenticationProperties
            //                {
            //                    IsPersistent = true,
            //                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
            //                };
            //            }

            //            await _signInManager.SignInAsync(user, props);
            //            if (_identityServerInteractionService.IsValidReturnUrl(returnUrl))
            //            {
            //                return Redirect(returnUrl);
            //            }


            //            return Redirect("~/");


            //        }
            //        ModelState.AddModelError(nameof(loginViewModel.Password), "Wrong Password");
            //    }
            //}

            return View(loginViewModel);
        }

        //public IActionResult MakeLogin()
        //{

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name,"jesse"),
        //        new Claim(ClaimTypes.Role, "admin")
        //    };

        //    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

        //    return Ok();
        //}

        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}