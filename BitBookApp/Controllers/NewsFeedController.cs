using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class NewsFeedController : Controller
    {
        // GET: NewsFeed

        UserLoginManager loginManager = new UserLoginManager();
        ProfileManager profileManager = new ProfileManager();
        public bool Layout()
        {

            
            var user = Session["User"] as User;

            if (user == null || Session["User"] == null)
            {
                return false;
            }

            if (loginManager.login(user))
            {
                FriendReqeust();
                ViewBag.FriendRequest = Session["FriendRequest"];
                ViewBag.CurrentUserProfile = user.Profile;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return true;
            }
            return false;  
        }
        CommentsManager commentsManager = new CommentsManager();
        PostManager postManager =  new PostManager();
        LikeManager likeManager = new LikeManager();
        public ActionResult Index()
        {
            var user = Session["User"] as User;

         

           
            ViewBag.Layout = "Index";



            if (Layout())
            {

                var listOfPost = commentsManager.CommetPostCombo(user.Id);
               ViewBag.currentUser = user.Id;

             
                ViewBag.PostList = listOfPost;




                return View("Index");
            }
            return View("error");
        }

        

        public RedirectToRouteResult Search()
        {
            return RedirectToAction("Index");
        }

        public ActionResult SearchView(string layout)
        {
            if (Layout())
            {
                return View(layout);
            }
            return View("error");
        }

        public ActionResult Error()
        {
            return View("error");
        }

        [HttpPost]
        public ActionResult  Search(string searchName , string layout)
        {

            var user = Session["User"] as User;
            ViewBag.currentUser = user.Id;
            if (searchName == null)
            {
                return SearchView(layout);
            }
            var listOfPost = postManager.GetAllPost(user.Id);


            ViewBag.PostList = listOfPost;
            var profileList = profileManager.GetProfilesByName(searchName);
            ViewBag.Search = profileList;
            var view = SearchView(layout);
            return view;
        }

        FriendRequestManager friendRequestManager =  new FriendRequestManager();
        public void FriendReqeust()
        {
           var  user =  (User)Session["User"];
           //get the list of FriendRequest Of a user 
           var friendRequest = friendRequestManager.GetFriendRequestListByid(user.Id);
            Session["FriendRequest"] = friendRequest;
            ViewBag.FriendRequest = Session["FriendRequest"];
        }
     
        public ActionResult LikeList(int postId)
        {
            var user = (User)Session["User"];


            var profile = profileManager.GetProfileByUserId(user.Id);

            ViewBag.CurrentUserProfile = profile;


           var like =  likeManager.GetUserInfoWhoLikeAPost(postId);
            foreach (var likelist in like)
            {
                likelist.UserId.Profile = profileManager.GetProfileByUserId(likelist.UserId.Id);

            }

            ViewBag.LikeList = like;
            

            return View("LikeList");

        }

        
        
       
     
    }
}