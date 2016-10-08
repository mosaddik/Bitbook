using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class LikeManager
    {
        LikeGetway likeGetway =  new LikeGetway();
        public bool LikeAPost(int currentUser, int post)
        {
            try
            {
                likeGetway.LikeAPost(currentUser, post);

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public int GetNumberOfLikes(int postId)
        {

            return likeGetway.GetLikesofAPost(postId).Count;
        }


        ProfileManager profileManager =  new ProfileManager();
        public List<Like> GetUserInfoWhoLikeAPost(int postId)
        {
            List<Like> likelist =  new List<Like>();

            foreach (var like in likeGetway.GetLikesofAPost(postId))
            {
                like.UserId.Profile = profileManager.GetProfileByUserId(like.UserId.Id);
                likelist.Add(like);
            }
         return likelist;
        }

        public bool ChekckThatLikeStatus(int currentUser , int postId)
        {
            return likeGetway.GetLikeByPostIdAndUseId(currentUser, postId);
        }

        public bool RemoveMyLike(int currentUser, int postId)
        {
            return likeGetway.RemoveMyLike(currentUser, postId);
        }
        PostManager postManager = new PostManager();



        public List<Post> LikePostCombo(int user)
        {
            var postlist =  new List<Post>();
            foreach (var post in postManager.GetAllPost(user))
            {
                
                post.NoOfLike = GetNumberOfLikes(post.Id);
                var likeList = GetUserInfoWhoLikeAPost(post.Id);
                foreach (var like in likeList)
                {
                    User users =  new User();
                    post.UserId.Add(like.UserId);   
                }
                
                 
                postlist.Add(post);
            }

            return postlist;
        }
	







    }
}