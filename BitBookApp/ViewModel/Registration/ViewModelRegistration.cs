using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitBookApp.ViewModel.Registration
{
    public class ViewModelRegistration
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Enter A Email Please")]
       [Remote("EmailValidaion", "Register", ErrorMessage = "Duplication Of Email ")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Please Enter Currect Email Address")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Enter a Password Please")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password Length 8 charecter Long ")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { set; get; }
        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { set; get; }
        [Required(ErrorMessage = "Enter Birth Day")]

        public DateTime BirthDay { set; get; }
        [Required(ErrorMessage = "Select birth Date Please")]

        public string Gender { set; get; }
        [DataType(DataType.Password)]

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Does not Match")]
        public string ConfirmPassword { get; set; }
    }
}