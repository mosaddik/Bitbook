using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public  string CoverPicture { get; set; }
        public string NickName { get; set; }

    }
}