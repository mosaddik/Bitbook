using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{

    
    public class PostManager
    {
        private StatusGetway statusGetway =  new StatusGetway();
        private  FriendsManager friendsManager =  new FriendsManager();

        public bool MakeAPost(Post post,int currentUser)
        {
            return statusGetway
                .StatusPost(post, currentUser);
        }

        public List<Post> GetOnlyMyPost(int currentUser)
        {
            var newPost = new List<Post>();

            foreach (var  MyPost in   statusGetway.GetStatusByUserId(currentUser))
            {
                MyPost.User.Profile = profileManager.GetProfileByUserId(MyPost.User.Id);
                newPost.Add(MyPost);
            }
            return newPost;
        }
        ProfileManager profileManager =  new ProfileManager();
        public List<Post> GetFriendsPost(int currentUser)
        {
            List<Post> postList  =  new List<Post>();
            //get friend's
            foreach (var friends in friendsManager.FriendListByCurrentuser(currentUser))
            {
                var listOfPost = statusGetway.GetStatusByUserId(friends.Friend2.Id);
                foreach (var posts in listOfPost)
                {
                    Post post =  new Post();
                    post.UserPost = posts.UserPost;
                    post.Id = posts.Id;
                    post.User.Id = posts.User.Id;
                    post.User.Profile = profileManager.GetProfileByUserId(posts.User.Id);
                    post.PostPicture = posts.PostPicture;
                    postList.Add(post);
                }           
            }
            return postList;
        }

        public List<Post> GetAllPost(int currentUser)
        {
            var postList = new List<Post>();

            try
            {
                 postList = GetFriendsPost(currentUser);

            }
            catch (Exception)
            {
                
                 postList =  new List<Post>();
            }
           
            foreach (var myPost in GetOnlyMyPost(currentUser))
            {

                postList.Add(myPost);         
            }
            postList.Reverse();

            return postList;
        }

        public bool RemovePost(int id)
        {

           return statusGetway.RemovePost(id);

        }
        

    }
}