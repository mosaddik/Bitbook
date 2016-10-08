using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class UserManager
    {
        private UserGetway userGetway = new UserGetway();
        private ProfileGetWay profileGetWay = new ProfileGetWay();
        public bool SaveUser(User user)
        {
             profileGetWay.SaveProfile(user.Profile);
            bool isSave = false;
            var userProfile = profileGetWay.GetRecentAddedData();
            user.Profile = userProfile;

            userGetway.SaveUser(user);
            return isSave;
        }

        public User GetUserByProfileId(int proifleId)
        {
            return userGetway.GetListofUserByProfileId(proifleId);
        }       

        public User GetUserByEmail(User user)
        {
            

            if (userGetway.GetUsers() == null)
            {
                return null;
            }
            foreach (var oldUser in userGetway.GetUsers())
            {
                if (user.Email == oldUser.Email)
                {
                    return oldUser;
                }

            }

            return null;
        }

        public bool IsEmailExitst(User user)
        {

            if (GetUserByEmail(user)!=null)
            {
                return true;
            }

            return false;
        }


        public bool SentValidationEmail(User user)
        {
            bool isExcucted = true;
            try
            {
                 var validationCode = 12345;
                var link = "<a href='conroller/action?'" + validationCode + "</a>";

                var body =
                    @"
                            Hi!
                            You just need to make one more click. Once you have confirmed your account you will be able to start receiving daily rewards for your online activities you do every day anyway. That's what SiteTalk is all about. See you on board! " 
                   ;

                var messege = new MailMessage();
                messege.To.Add(new MailAddress(user.Email));
                messege.From = new MailAddress("mosaddikbinhafiz@gmail.com");
                messege.Body = body+link;
                messege.Subject = "Please Confirm Your Email BitBOok";

                messege.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var creditial = new NetworkCredential()
                    {
                        UserName = "mosaddikbinhafiz@gmail.com",
                        Password = "falcon19",

                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = creditial;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;

                    smtp.EnableSsl = true;
                    smtp.Send(messege);
                }
            }
            catch (Exception e)
            {
                isExcucted = false;
            }

            return isExcucted;
        }


        public bool ConfirmValidation(int validationCode)
        {
            bool isValidte = false;
            var code = new Random(1).Next(10000, 1000000);
            if (validationCode == code)
            {
                isValidte = true;
            }
            return isValidte;
        }

    }
}
