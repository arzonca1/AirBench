using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using AirBench.Models;
using AirBench.Providers;
using AirBench.Results;
using System.Web.Mvc;
using System.Web.Security;
using AirBench.Data;

namespace AirBench.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginFormModel formModel = new LoginFormModel();
            return View(formModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Login(LoginFormModel formModel)
        {
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                var _repository = new UserRepository();
                // TODO Get the user record from the database by their email.
                User user = await _repository.GetUser(formModel.Email);

                

                // If we didn't get a user back from the database
                // or if the provided password doesn't match the password stored in the database
                // then login failed.
                if (user == null || !BCrypt.Net.BCrypt.Verify(formModel.Password, user.HashedPassword))
                {
                    ModelState.AddModelError("", "Invalid login credentials");
                }
            }

            if (ModelState.IsValid)
            {
                // Login the user.
                FormsAuthentication.SetAuthCookie(formModel.Email, false);

                // Send them to the home page.
                return RedirectToAction("Index", "Benches");
            }

            return View(formModel);

        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterFormModel formModel = new RegisterFormModel();
            return View(formModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Register(RegisterFormModel formModel)
        {
            var _repository = new UserRepository();
            if (ModelState.IsValidField("Email") && ModelState.IsValidField("Password"))
            {
                
                
                User user = await _repository.GetUser(formModel.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "This email is already in use.");
                }
                if (!(new VerifyEmail(formModel.Email).IsValid()))
                {
                    ModelState.AddModelError("", "This email is invalid");
                }
                if (string.IsNullOrWhiteSpace(formModel.FirstName))
                {
                    ModelState.AddModelError("", "Name can't be empty");
                }
                if (string.IsNullOrWhiteSpace(formModel.LastName))
                {
                    ModelState.AddModelError("", "Name can't be empty");
                }

            }

            if (ModelState.IsValid)
            {
                //create new user 

                User user = await _repository.RegisterUser(formModel.Email, BCrypt.Net.BCrypt.HashPassword(formModel.Password, 12), formModel.FirstName, formModel.LastName);
                

                // Login the new user.
                FormsAuthentication.SetAuthCookie(formModel.Email, false);

                // Send them to the home page.
                return RedirectToAction("Index", "Home");
            }

            return View(formModel);

        }

        [HttpPost]
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }

}
