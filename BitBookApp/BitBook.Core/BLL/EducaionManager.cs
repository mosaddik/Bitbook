using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class EducaionManager
    {
        EducationGetway educationGetway = new EducationGetway();
        private bool Save(Education  education, User currentUser)
        {
            try
            {
                //save work 
                educationGetway.Save(education);
                //get recent added data 
                var recentEducation = educationGetway.GetRecentAddedData();
                //make a one to many relation to User
                currentUser.Education = recentEducation;
                educationGetway.SetAOneToOneConnectionToUser(currentUser);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool Update(Education education, User currentUser)
        {
            try
            {
                var userInfo = educationGetway.GetUserInfoById(currentUser);
                education.Id = userInfo.Education.Id;
                educationGetway.Update(education);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public void MakeADecisionSaveOrUpdate(Education education, User currentUser)
        {

            //get id form work 
            var fullUserinfo = educationGetway.GetUserInfoById(currentUser);

            currentUser.Education.Id = fullUserinfo.Education.Id;
            education.Id = currentUser.Education.Id;

            if (education.Id == 0)
            {
                Save(education, currentUser);

            }
            else
            {
                Update(education, currentUser);
            }
        }

        public Education GetEducationByUser(User currentuser)
        {

            var user = educationGetway.GetUserInfoById(currentuser);
            currentuser.Education.Id = user.Education.Id;
            return educationGetway.GetEducationByUser(currentuser);

        }
    }
}