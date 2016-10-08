using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class PasswordManger
    {
        private UserGetway userGetway = new UserGetway();
        private UserManager userManager = new UserManager();
        private  UserLoginManager loginManager = new UserLoginManager();



        public bool ChangeUserPassword(User user)
        {
            return userGetway.UpdatePassword(user);
        }




    }
}