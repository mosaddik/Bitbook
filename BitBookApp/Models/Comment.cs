using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserComment { get; set; }
        public User User { get; set; }
        public Post post { get; set; }


        public Comment()
        {
            User = new User();
            post =  new Post();
        }
    }
}