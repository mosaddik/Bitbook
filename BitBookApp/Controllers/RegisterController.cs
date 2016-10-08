using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;
using BitBookApp.ViewModel.Registration;

namespace BitBookApp.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        UserManager userManager = new UserManager();
        

        public ActionResult Registation()
        {
            ViewBag.Days = DaysDropDown();
            ViewBag.Months = MonthDropDown();
            ViewBag.Years = YearList();
            return View();
        }



        [HttpPost]
        public RedirectToRouteResult RegistationComplete(ViewModelRegistrationLogin registerLogin, string day, string month, string year)
        {
            ViewBag.Email = registerLogin.ViewModelRegistration.Email;
            var user = new User();
            user.Email = registerLogin.ViewModelRegistration.Email;
            user.Password = registerLogin.ViewModelRegistration.Password;
            user.ValidateEmail = "false";
            user.Profile.FristName = registerLogin.ViewModelRegistration.FirstName;
            user.Profile.LastName = registerLogin.ViewModelRegistration.LastName;
            user.Profile.Picture = "";
            user.Profile.Gender = registerLogin.ViewModelRegistration.Gender;









            var dateTime = new DateTime( Convert.ToInt32(year), Convert.ToInt32(month),Convert.ToInt32(day));
            user.Profile.BirthDay = dateTime;
           
            userManager.SaveUser(user);
            userManager.SentValidationEmail(user);

            return RedirectToAction("Registation", "Register");

        }


        



        public List<SelectListItem> DaysDropDown()
        {
            var days = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Days", Value = ""}
            };


            for (int i = 1; i <= 31; i++)
            {
                var day = new SelectListItem() {Text = Convert.ToString(i), Value = Convert.ToString(i)};
                days.Add(day);
            }
            return days;
        }

        public List<SelectListItem> MonthDropDown()
        {
            var monthList = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Month", Value = ""}
            };
            var monthArray = new string[]
            {
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October",
                "November", "December"
            };

            for (int i = 1; i <= 12; i++)
            {
                var year = new SelectListItem() {Text = monthArray[i - 1], Value = Convert.ToString(i)};
                monthList.Add(year);
            }
            return monthList;
        }

        public List<SelectListItem> YearList()
        {
            var yearList = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Year", Value = ""}
            };


            for (int i = 1900; i <= DateTime.Today.Year; i++)
            {
                var year = new SelectListItem() {Text = Convert.ToString(i), Value = Convert.ToString(i)};
                yearList.Add(year);
            }
            return yearList;


        }

        public JsonResult EmailValidaion(ViewModelRegistrationLogin registration)
        {
            bool isValidate = true;
            var user = new User()
            {
                Email = registration.ViewModelRegistration.Email,
                Password = registration.ViewModelRegistration.Password
               
            };

            if (userManager.IsEmailExitst(user))
            {
                isValidate = false;
                return Json(isValidate, JsonRequestBehavior.AllowGet);
            }

            return Json(isValidate, JsonRequestBehavior.AllowGet);
            ;
        }


    }


}
