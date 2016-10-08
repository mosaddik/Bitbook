using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitBookApp.ViewModel.Registration
{
    public class ViewModelLogin
    { 
        [Required(ErrorMessage = "Enter a Email Address")]
        [Display(Name = "Email")]
     
        public string EmailForLogin { get; set; }
        [Required(ErrorMessage = "Enter a Password")]
        [Display(Name = "Password")]
        //[Remote("UserAuthentication", "Login", ErrorMessage = "Wrong UserName or password")]
        public string PasswordForLogin { get; set; }

    }
}