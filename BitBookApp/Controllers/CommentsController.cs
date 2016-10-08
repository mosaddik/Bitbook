using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;

namespace BitBookApp.Controllers
{
    public class CommentsController : Controller
    {
        CommentsManager commentsManager =  new CommentsManager();
        
        //
        // GET: /Comments/
        public RedirectToRouteResult Comment(string commets, string postId)
        {
           var user = (User)Session["User"];



            commentsManager.SaveACommets(Convert.ToInt32(postId), user.Id, commets);

            return RedirectToAction("Index", "NewsFeed");
        }
    }
}