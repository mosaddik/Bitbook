using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;


namespace BitBookApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string  Password { get; set; }
        public string ValidateEmail { get; set; }
        public Profile Profile { get; set; }
        public Education Education { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Hobby Hobby { get; set; }
        public List<Post> Posts { get; set; }
        public Work Work { get; set; }
        public Parents Parents { get; set; }

        public User()
        {
             Profile = new Profile();
             Education = new Education();
             Contact = new Contact();
             Address = new Address();
             Hobby = new Hobby();
             Posts = new List<Post>();
             Parents = new Parents();
             Work = new Work();
        }
    }
  
   
}