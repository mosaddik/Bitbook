using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.BLL
{
    public class CommentsManager
    {
        private CommentsGetway commentsGetway =  new CommentsGetway();
        private ProfileManager profileManager = new ProfileManager();
        private  PostManager postManager =  new PostManager();

        public bool SaveACommets(int postId,int currentUser,string comments)
        {
            var comment = new Comment
            {
                post = new Post() {Id = postId},
                User = new User() {Id = currentUser},
                UserComment = comments
            };
            return commentsGetway.SaveAComment(comment);
        }
        public List<Comment> GetUserInfoWhoCommetonThat(int postId)
        {
            List<Comment> commentList = new List<Comment>();

            foreach (var commets in commentsGetway.GetCommentsOfAPost(postId))
            {
                commets.User.Profile = profileManager.GetProfileByUserId(commets.User.Id);
                commentList.Add(commets);
            }
            return commentList;
        }
        LikeManager likeManager = new LikeManager();
        public List<Post> CommetPostCombo(int user)
        {
            var postlist = new List<Post>();
            foreach (var postList in likeManager.LikePostCombo(user))
            {
                Post post = new Post();
                post = postList;
                foreach (var commmet in GetUserInfoWhoCommetonThat(postList.Id))
                {
                    var commets  = new Comment();
                    commets = commmet;
                    postList.Comment.Add(commets);
                }
                Comment comment = new Comment();
                postlist.Add(post);
            }

            return postlist;
        }

    }
}