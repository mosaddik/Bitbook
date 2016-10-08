using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class UserLoginManager
    {
        UserGetway userGetway= new UserGetway();

        public bool login(User user)
        {
            if (userGetway.GetUsers() == null)
            {
                return false;
            }
            foreach (var oldUser in userGetway.GetUsers())
            {
                if (oldUser.Email == user.Email && oldUser.Password == user.Password)
                {
                    
                    return true;

                }
                
            }
            return false;
        }

        public bool CheckSession(User user)
        {
           bool isSessionExist = false;
            if (user != null)
            {
                isSessionExist = true;
            }
            return isSessionExist;
        }
        
    }
}