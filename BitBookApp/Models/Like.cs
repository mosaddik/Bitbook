using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Like
    { 
        public User UserId { get; set; }
        public Post PostId { get; set; }

        public Like()
        {
            UserId = new User();
            PostId =  new Post();

        }
    
    }

}