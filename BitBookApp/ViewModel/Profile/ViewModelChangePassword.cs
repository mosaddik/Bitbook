using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BitBookApp.ViewModel.Profile
{
    public class ViewModelChangePassword
    {
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Does not Match")]
        public string ConfirmPassword { get; set; }

    }
}