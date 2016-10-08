using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitBookApp.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public User User1  { get; set; }
        public User User2 { get; set; }



        public FriendRequest()
        {
            User1 =  new User();
            User2 = new User();
        }

    }
}