using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class HobbyManager
    {
        HobbyGetway hobbyGetway = new HobbyGetway();
        private bool Save(Hobby hobby, User currentUser)
        {
            try
            {
                //save work 
                hobbyGetway.Save(hobby);
                //get recent added data 
                var recentWork = hobbyGetway.GetRecentAddedData();
                //make a one to many relation to User
                currentUser.Hobby = recentWork;
                hobbyGetway.SetAOneToOneConnectionToUser(currentUser);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool Update(Hobby hobby, User currentUser)
        {
            try
            {
                var userInfo = hobbyGetway.GetUserInfoById(currentUser);
                hobby.Id = userInfo.Hobby.Id;
                hobbyGetway.Update(hobby);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public void MakeADecisionSaveOrUpdate(Hobby hobby, User currentUser)
        {

            //get id form work 
            var fullUserinfo = hobbyGetway.GetUserInfoById(currentUser);

            currentUser.Hobby.Id = fullUserinfo.Hobby.Id;

            hobby.Id = currentUser.Hobby.Id;

            if (hobby.Id == 0)
            {
                Save(hobby, currentUser);

            }
            else
            {
                Update(hobby, currentUser);
            }
        }

        public Hobby GetHobyByUser(User currentuser)
        {

            var user = hobbyGetway.GetUserInfoById(currentuser);
            currentuser.Hobby.Id = user.Hobby.Id;
            return hobbyGetway.GetHobyByUser(currentuser);

        }
    }
}