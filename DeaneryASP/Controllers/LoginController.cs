using DeaneryASP.Models;
using DeaneryASP.Models.Storage;
using DeaneryASP.Models.Storage.Impl;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DeaneryASP.Controllers
{
    public class LoginController : Controller
    {
        private ILoginStorage storage;

        

        public LoginController()
        {
            storage = new LoginStorage();

        }

        public LoginController(ILoginStorage storage)
        {
            this.storage = storage;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int user = storage.WhoLogins(model.Login, model.Password);
                    if (user == LoginStorage.ADMINISTRATOR)
                    {
                        addCookie(model.Login, "Admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user == LoginStorage.PROFESSOR)
                    {
                        addCookie(model.Login, "Professor");
                        return RedirectToAction("Index", "Professor");
                    }
                    else if (user == LoginStorage.STUDENT)
                    {
                        addCookie(model.Login, "Student");
                        return RedirectToAction("Index", "Student");
                    }
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("", "Błędny login lub hasło!");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private void addCookie(string username, string roles)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                          1,
                          username,  //user id
                          DateTime.Now,
                          DateTime.Now.AddMinutes(20),  // expiry
                          false,  //do not remember
                          roles,
                          "/");
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                               FormsAuthentication.Encrypt(authTicket));
            if (Response != null && Response.Cookies != null)
            {
                Response.Cookies.Add(cookie);
            }
        }
    }
    #endregion
}