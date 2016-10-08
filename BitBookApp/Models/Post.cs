using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserPost { get; set; }
        public string PostPicture { get; set; }
        public int NoOfLike { get; set; }
        public User User { get; set; }
        public List<User> UserId { get; set; }

        public List<Comment> Comment { get; set; }


        public Post()
        {            
            UserId =   new List<User>();
            User =  new User();
            Comment = new List<Comment>();
        }
    }
}