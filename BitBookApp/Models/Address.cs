using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Hometown { get; set; }
        public string  Country { get; set; }
    }
}