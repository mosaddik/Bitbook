using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class LikeController : Controller
    {
        //
        // GET: /Like/
        public RedirectToRouteResult Index()
        {





            return RedirectToAction("Index", "NewsFeed");
        }
        LikeManager likeManager =  new LikeManager();
        public RedirectToRouteResult LikeAPost(int postId)
        {

            var user = Session["User"] as User;

              
            if(!likeManager.ChekckThatLikeStatus(user.Id , postId))
            {
                likeManager.LikeAPost(user.Id, postId);
            }
        else
            {
                likeManager.RemoveMyLike(user.Id, postId);

            }
                


               var numberOfLike = likeManager.GetNumberOfLikes(postId);
               @ViewBag.LikeCounter = numberOfLike;


            return RedirectToAction("Index", "NewsFeed");
        }

        
	}
}