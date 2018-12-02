using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsentServer.ViewModels;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConsentServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;
        public AccountController(TestUserStore users)
        {
            this._users = users;
        }

        //内部跳转
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //添加验证错误
        private void AddError(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public IActionResult Register(string returnUrl=null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        //public IActionResult Login(string returnUrl = null)
        //{
        //    ViewData["returnUrl"] = returnUrl;
        //    return View();
        //}

        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["returnUrl"] = returnUrl;
                var user = _users.FindByUsername(loginViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.Email), "UserName not exists");
                }
                else
                {
                    if (_users.ValidateCredentials(loginViewModel.Email, loginViewModel.Password))
                    {
                        var prop = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };

                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, user.SubjectId, user.Username, prop);
                    }

                }
                return RedirectToLocal(returnUrl);

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}