using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class FriendsManager
    {
        FriendsGetway friendsGetway =  new FriendsGetway();
        public bool MakeFriend(int user1, int friend1)
        {
            try
            {
                var friendRequest = new FriendRequest()
                {
                    User1 = new User() {Id = user1},
                    User2 = new User() {Id = friend1},
                };
                friendsGetway.Add(friendRequest);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
       }

        public List<Friends> FriendListByCurrentuser(int currentUser)
        {
            var friendList = GetAllFriends(currentUser);

            foreach (var friends in friendsGetway.GetMoreFriend(currentUser))
            {
                var newFriends = new Friends();
                newFriends = friends;
                friendList.Add(newFriends);

            }
            return friendList;
        }

        private List<Friends> GetAllFriends(int currentUser)
        {
            return friendsGetway.GetAllFriendList(currentUser);

        }

        public bool CheckFriendList(int user1, int friend1)
        {

            foreach (var friends in FriendListByCurrentuser(user1))
            {
                if (friend1 == friends.Friend2.Id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Unfriend(int unfriend , int myid)
        {

            Friends friends = new Friends()
            {
                Friend1 =  new User(){ Id =  myid},
                Friend2 =  new User(){ Id =  unfriend}
            };
            Friends friend2 = new Friends()
            {
                Friend2 = new User() { Id = myid },
                Friend1 = new User() { Id = unfriend }
            };

            try
            {
                friendsGetway.Remove(friends);
                friendsGetway.Remove(friend2);
                return true;
            }
            catch (Exception e)
            {
                return true;
            }

        }



         



    }
}