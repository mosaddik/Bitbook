using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class ProfileManager
    {

        ProfileGetWay profileGetWay = new ProfileGetWay();
        UserGetway userGetway = new UserGetway();

       

        public bool SaveProfile(Profile profile)
        {
           return profileGetWay.SaveProfile(profile);
        }

       public  List<Profile> GetProfilesByName(string name)
        {
            return profileGetWay.GetProfilesByName(name);
        }

        public List<Profile> GetListOfProfileByUserId(List<Friends> user)
        {
            List<Profile> profiles =  new List<Profile>();

            try
            {

                foreach (var userelements in user)
                {
                    Profile profile = new Profile();

                    profile = GetProfileByUserId(userelements.Friend2.Id);
                    profiles.Add(profile);
                }
            }
            catch (Exception e)
            {
                profiles = null;
            }

            return profiles;
        }



        public Profile GetRecentAddedData()
        {
            return profileGetWay.GetRecentAddedData();

        }

        public Profile GetProfileById(int id)
        {
            return profileGetWay.GetProfileById(id);
        }

        public bool UpdateProfile(User currentUser, Profile profile)
        {
            return profileGetWay.UpdateProfile(currentUser, profile);
        }

        public Profile GetProfileByUserId(int id)
        {

           var profileId = profileGetWay.GetProfileId(id);
            return profileGetWay.GetProfileById(profileId);

        }





        //repteared search code 

    }
}