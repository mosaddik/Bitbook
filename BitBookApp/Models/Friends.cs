using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Friends
    {
        public int Id { get; set; }
        public User Friend1 { get; set; }
        public User Friend2 { get; set; }



        public Friends()
        {
            Friend1 = new User();
            Friend2 = new User();
        }
    }
}