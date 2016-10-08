using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class WorkMnager
    {
        WorkGetway workGetway = new WorkGetway();
    

        private bool SaveWork(Work work ,User currentUser)
        {
            try
            {
                //save work 
                workGetway.SaveWork(work);
                //get recent added data 
                var recentWork = workGetway.GetRecentAddedData();
                //make a one to many relation to User
                currentUser.Work = recentWork;
                workGetway.SetAOneToOneConnectionToUser(currentUser);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool UpdateWork(Work work,User currentUser)
        {
            try
            {
                var userInfo = workGetway.GetUserInfoById(currentUser);
                work.Id = userInfo.Work.Id;
                workGetway.UpdateWork(work);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public void MakeADecisionSaveOrUpdate(Work work , User currentUser)
        {
          
            //get id form work 
            var fullUserinfo = workGetway.GetUserInfoById(currentUser);

            currentUser.Work.Id = fullUserinfo.Work.Id; 
            var oldWorkInfo = workGetway.GetWorkByUser(currentUser);
            work.Id = oldWorkInfo.Id;

            if (work.Id == 0)
            {
                SaveWork(work, currentUser);

            }
            else
            {
                UpdateWork(work, currentUser);
            }
        }

        public Work GetWorkByUser(User currentuser)
        {

             var user = workGetway.GetUserInfoById(currentuser);
             currentuser.Work.Id = user.Work.Id;
             return  workGetway.GetWorkByUser(currentuser);

        }


    }
}


