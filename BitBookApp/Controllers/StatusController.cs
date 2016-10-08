using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.Models;
using BitBookApp.ViewModel;

namespace BitBookApp.Controllers
{

    public class StatusController : Controller
    {
        //
        // GET: /Status/
        PostManager postManager =  new PostManager();
        
        [HttpPost]
        public RedirectToRouteResult MakeAPost(ViewModelPost viewModelPost , string post)
        {
         
            var user = (User)Session["User"];
             var newPost = new Post();
            string path = "";
            var profilePic = viewModelPost.Picture;

            if (profilePic != null)
            {
                string pic = System.IO.Path.GetFileName(profilePic.FileName);
                path = System.IO.Path.Combine(
                    Server.MapPath("~/Image/ProfilePic"), pic);
                // file is uploaded
                profilePic.SaveAs(path);
               
                newPost.PostPicture = viewModelPost.Picture.FileName;
                newPost.UserPost = post;
            }
            else
            {
                 newPost = new Post();
                newPost.PostPicture = "a";
                newPost.UserPost = post;
            }
            //save path to  database 
            postManager.MakeAPost(newPost,user.Id);
            return RedirectToAction("Index", "NewsFeed");

        }

        public RedirectToRouteResult RemovePost(string removePost)
        {
            postManager.RemovePost(Convert.ToInt32(removePost));
            return RedirectToAction("Index", "NewsFeed");
        }

        
	}
}