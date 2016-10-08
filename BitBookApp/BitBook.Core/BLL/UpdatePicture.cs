using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class UpdatePicture
    {
        ProfileGetWay profileGetWay =  new ProfileGetWay();


        public bool UpdateProfilePic(User currentUser)
        {
            try
            {
                //get profile id of the current user 
                int profileId = profileGetWay.GetProfileId(currentUser.Id);
                currentUser.Profile.Id = profileId;
                profileGetWay.UpdateProfilePicture(currentUser.Profile);
                return true;
            }
            catch (Exception)
            {
                
                return false;
                
            }
                 
        }

       public bool UpdateCoverPic(User currentUser, Profile profile)
     {

           try
           {
               int profileId = profileGetWay.GetProfileId(currentUser.Id);
               profile.Id = profileId;
               profileGetWay.UpdateCoverPhoto(profile);
               return true;
           }
           catch(Exception)
         {
               return false;
             
         }



      }











    }
}