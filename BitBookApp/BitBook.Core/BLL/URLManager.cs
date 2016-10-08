using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class URLManager
    {
        UserLoginManager login = new UserLoginManager();
        public bool ValidateURL(User user)
        {
           return login.login(user);
        }



    }
}