using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class ParentManger
    {
        UserGetway userGetway = new UserGetway();
        ProfileGetWay profileGetWay = new ProfileGetWay();
        ParentGetway parentGetway = new ParentGetway();



        private bool SaveParent(Parents parents,User sessonUser)
        {
            bool isSave = false;
            try
            {
                parentGetway.SaveProfile(parents);
                var user = new User();
                user.Parents = parentGetway.GetRecentAddedData();
                user.Id = sessonUser.Id;
                parentGetway.SetRelationShipToUser(user);
                isSave = true;               
            }
            catch (Exception)
            {

                isSave = false;
            }

            return isSave;
        }

        private bool UpdateParents(Parents parents)
        {

            return parentGetway.UpdateParents(parents);
        }

        public void MakeDesitionUpdateorSave(Parents parents,User sessonUser)
        {     
            
            //check Parents Aginest this User Has Exist or Not 
            parents.Id = parentGetway.GetParentId(sessonUser);


            if (parents.Id == 0)
            {
                SaveParent(parents,sessonUser);
            }
            else
            {
                UpdateParents(parents);
            }
        }


        public Parents GetParentsById(User currentUser)
        {
            currentUser.Parents.Id = parentGetway.GetParentId(currentUser);
            return parentGetway.GetParentById(currentUser);
        }

    }
}