using System.Collections.Generic;
using System.Security.Claims;
using belajarnetcoremvc.Controllers;
using belajarnetcoremvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class SecurityController : Controller
    {
        public IActionResult Denied(){
            return View();
        }

         public IActionResult Login()
        {
            ViewData["Title"]="Login";
            ViewData["Message"]="Silakan isi login form di bawah ini.";
            SignOut(CookieAuthenticationDefaults.AuthenticationScheme).ExecuteResult(ControllerContext);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // set claimsidentity
                var claims =new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, loginViewModel.UserName));
                claims.Add(new Claim(ClaimTypes.Name, loginViewModel.UserName));
                claims.Add(new Claim(ClaimTypes.Email, loginViewModel.UserName));
                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                
                // set authentication properties
                var authProps = new AuthenticationProperties{
                    IsPersistent=false,
                };
                if(!string.IsNullOrEmpty(ReturnUrl))
                    authProps.RedirectUri=ReturnUrl;
                
                var s=  SignIn(principal,CookieAuthenticationDefaults.AuthenticationScheme);
                 s.Properties=authProps;
                 return s;
            }
            return View(loginViewModel);
        }
         public IActionResult SignOut()
         {
             var props = new AuthenticationProperties{
                 RedirectUri="/Home/Index"
             };
             var s= SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
             s.Properties=props;
             return s;
         }
  private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        } 
    }