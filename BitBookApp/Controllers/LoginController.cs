using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;
using BitBookApp.ViewModel.Registration;

namespace BitBookApp.Controllers
{
    public class LoginController : Controller
    {
        //
        UserLoginManager loginManager = new UserLoginManager();
        UserManager userManager = new UserManager();
        ProfileManager profileManager  = new ProfileManager();
        // GET: /Login/
        public RedirectResult Login(ViewModelRegistrationLogin login)
        {
            User user = null;
            User newUser = null;
            try
            {
                 user = new User()
                {
                    Email = login.ViewModelLogin.EmailForLogin,
                    Password = login.ViewModelLogin.PasswordForLogin
                };

                 newUser = userManager.GetUserByEmail(user);
                newUser.Profile = profileManager.GetProfileById(newUser.Profile.Id);
            }
            catch (Exception e)
            {
                 user = new User()
            {
                Email  =  login.ViewModelLogin.EmailForLogin,
                Password = login.ViewModelLogin.PasswordForLogin
            };
             newUser = userManager.GetUserByEmail(user);

            }

            if (loginManager.login(user))
            {

                Session["User"] = newUser;
                return Redirect("/NewsFeed/Index");
            }
            else
            {
                return Redirect("/Register/Registation");
            }
        }

        public JsonResult UserAuthentication(ViewModelRegistrationLogin login)         
        {
            var user = new User()
            {
                Email =  login.ViewModelLogin.EmailForLogin,
                Password = login.ViewModelLogin.PasswordForLogin
            };
            if (loginManager.login(user))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public RedirectResult Logout()
        {
            Session["User"] = null;
            return Redirect("../Register/Registation");
        }

    }
}