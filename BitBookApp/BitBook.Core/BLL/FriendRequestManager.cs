using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class FriendRequestManager
    {
        FriendRequestGetway friendRequestGetway  =  new FriendRequestGetway();
        public bool SentFriendReqeust(FriendRequest friendRequest)
        {
            //friend Request sent to user 

            friendRequestGetway.Add(friendRequest);


            return true;
        }

        public List<FriendRequest> GetFriendRequests(int id)
        {
            return friendRequestGetway.GetAllFriendReqeust(id);
        }

        public bool CheckDuplication(int user1, int user2)
        {
            bool isDuplicate = false;

            try
            {
                foreach (var friendReqeust in friendRequestGetway.GetAllFriendReqeust(user1))
                {
                    if (friendReqeust.User2.Id == user2)
                    {
                        isDuplicate = true;
                    }
                    else
                    {
                        isDuplicate = false;
                    }

                }





            }
            catch (Exception e)
            {
                isDuplicate = false;
            }


            return isDuplicate;
        }

        ProfileManager profileManager =  new ProfileManager();
        public List<User> GetFriendRequestListByid(int id)
        {
            var user  =  new User();
            var userList   =  new List<User>();
            foreach (var friendRequest in friendRequestGetway.GetAllFriendRequestsById(id))
            {
                user  =  new User();
                user = friendRequest.User1;
                user.Profile = profileManager.GetProfileByUserId(user.Id);
                userList.Add(user);
            }

            return userList;
        }

        public int ConfirmFriendRequest(int userId,int getRequest)
        {
           int isExist = 0;
            foreach (var getRequestUser in  GetFriendRequestListByid(userId))
            {
                if (getRequestUser.Id == getRequest)
                {
                    return 1;
                }                
            }           
            return isExist;

        }

        public bool RemoveRequest(int user1 , int user2)
        {
            try
            {
                friendRequestGetway.Remove(user1, user2);
                friendRequestGetway.FullRemove(user1, user2);
                return true;
            }
            catch (Exception)
            {

                return false;

            }
            


        }










    }
}