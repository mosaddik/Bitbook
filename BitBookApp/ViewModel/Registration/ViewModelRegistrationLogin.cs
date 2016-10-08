using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BitBookApp.ViewModel;

namespace BitBookApp.ViewModel.Registration
{
    public class ViewModelRegistrationLogin
    {
        [Required]
        public ViewModelRegistration ViewModelRegistration { get; set; }
        [Required]
        public ViewModelLogin ViewModelLogin { get; set; }
    }
}