using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class FriendRequestController : Controller
    {
        //
        // GET: /FriendRequest/


        FriendRequestManager friendRequestManager = new FriendRequestManager();
        UserManager userManager = new UserManager();
        ProfileManager profileManager = new ProfileManager();

        [HttpPost]
        public RedirectToRouteResult Index(string friendReqeust)
        {


            var user = (User) Session["User"];

            var friendReqeusts = new FriendRequest()
            {
                User1 = user,
                User2 = new User()
                {
                    Id = Convert.ToInt32(friendReqeust)
                }
            };
            friendRequestManager.SentFriendReqeust(friendReqeusts);
            var Profile = profileManager.GetProfileByUserId(Convert.ToInt32(friendReqeust));
            return RedirectToAction("Index", "Profile", new {id = Profile.Id});
        }

        FriendsManager friendsManager = new FriendsManager();
        UserManager _userManager = new UserManager();

        public RedirectToRouteResult MakeDicisionConfirmOrCancel(string button, string friend1, string user1ProfileId)
        {
            string getbutton = button;

            var user = _userManager.GetUserByProfileId(Convert.ToInt32(user1ProfileId));



            if (button == "1")
            {
                friendsManager.MakeFriend(user.Id, Convert.ToInt32(friend1));
                friendRequestManager.RemoveRequest(user.Id, Convert.ToInt32(friend1));
            }
            if (button == "0")
            {
                friendRequestManager.RemoveRequest(user.Id, Convert.ToInt32(friend1));
            }

            var profile = profileManager.GetProfileByUserId(Convert.ToInt32(friend1));

            return RedirectToAction("Index", "Profile", new {id = profile.Id});
        }

        UserLoginManager login = new UserLoginManager();

        public ActionResult FriendList()
        {

            var user = (User) Session["User"];

         
            if (login.CheckSession(user))
            {
                var friends = friendsManager.FriendListByCurrentuser(user.Id);

                
                var friendList = profileManager.GetListOfProfileByUserId(friends);

                ViewBag.FriendList = friendList;
                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;

                return View("FriendList");
            }
            return View("error");
        }
        [HttpPost]
        public RedirectToRouteResult Unfriend(string unfriend)
        {
            var user = (User) Session["User"];
            var unfriendUserId = _userManager.GetUserByProfileId(Convert.ToInt32(unfriend));
            friendsManager.Unfriend(user.Id, unfriendUserId.Id);

            return RedirectToAction("FriendList");
        }
    }
}