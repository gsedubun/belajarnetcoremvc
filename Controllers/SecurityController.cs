using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using belajarnetcoremvc.Controllers;
using belajarnetcoremvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class SecurityController : Controller
    {
    private belajarDbContext Db;

    public SecurityController(belajarDbContext context)
        {
            this.Db = context;
        }
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
                var user = Db.TblUser.SingleOrDefault(d=> d.UserName==loginViewModel.UserName && d.Password==loginViewModel.Password);
                if(user!=null)
                {
                    var role  = (from ur in Db.TblUserRole
                    join r in Db.TblRole on ur.TblRole equals r
                    select new {Role= r.RoleName, ur.TblUser}
                    ).ToList();

                    if(role==null)
                            return View(loginViewModel);

                    var claims =new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, loginViewModel.UserName));
                    claims.Add(new Claim(ClaimTypes.Name, loginViewModel.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, loginViewModel.UserName));
                    foreach (var r in role)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, r.Role));
                    }
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
                else{
                    //ModelState.AddModelError("InvalidLogin", new System.Exception("username and password is invalid."));
                    ModelState.AddModelError(string.Empty, "Username and Password is invalid.");
                    //ViewData["Message"]="Username and Password is invalid.";

                }
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