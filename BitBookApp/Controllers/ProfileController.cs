using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitBookApp.BitBook.Core.BLL;
using BitBookApp.BitBook.Core.DAL;
using BitBookApp.Models;
using BitBookApp.ViewModel.Profile;


namespace BitBookApp.Controllers
{
    public class ProfileController : Controller
    {
        UserLoginManager login  =  new UserLoginManager();
        ParentManger parentManger = new ParentManger();
        ProfileManager profileManager =  new ProfileManager();
        AddressManager addressManager =  new AddressManager();
        HobbyManager hobbyManager = new HobbyManager();
        WorkMnager workMnager = new WorkMnager();
        EducaionManager educaionManager = new EducaionManager();
        UpdatePicture updatePicture = new UpdatePicture();
        FriendRequestManager friendRequestManager = new FriendRequestManager();
        UserManager userManager = new UserManager();
        FriendsManager friendsManager = new FriendsManager();
        // GET: /Profile/
        public ActionResult Index(string id)
        {
            var user = (User) Session["User"];
            


            Profile profile = null;
           

            if (login.CheckSession(user))
            {
               
                var sentTORequest =  userManager.GetUserByProfileId(Convert.ToInt32(id));
                ViewBag.User = "";
                Session["User2"] = sentTORequest.Id.ToString();
                   


                profile = profileManager.GetProfileByUserId(user.Id);

                if (profile.Id == Convert.ToInt32(id))
                {
                    ViewBag.ThisUser = 1;
                }
                else
                {
                    ViewBag.ThisUser = 0;
                }
                ViewBag.CurrentUserProfile = user.Profile;
                ViewBag.Layout = "~/Views/Profile/Index.cshtml";
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;

                if (friendRequestManager.CheckDuplication(user.Id, sentTORequest.Id))
                {
                    ViewBag.FriendReqeust = 1;
                }
                

                if (id != null)
                {
                    ViewBag.User = (string)Session["User2"];
                   profile = profileManager.GetProfileById(Convert.ToInt32(id));
                }
               
               
               //check  the friend  list
                var userId = userManager.GetUserByProfileId(Convert.ToInt32(id));
                var friendList = friendsManager.CheckFriendList(user.Id, userId.Id);
                
                if (friendList)
                {
                    ViewBag.Friend = 1;
                }
                else
                {
                    ViewBag.Friend = 0;
                }

                 
 
               ViewBag.Profile = profile;


                var currentProfileId = profileManager.GetProfileByUserId(user.Id);
                var getUserId = userManager.GetUserByProfileId(Convert.ToInt32(id));

                Work work = null;
                Education education = null;
                Address address = null;
                Hobby hobby = null;

                if (id == null)
                {
                     work = workMnager.GetWorkByUser(user);
                     education = educaionManager.GetEducationByUser(user);
                     address = addressManager.GetAddressByUserId(user);
                     hobby = hobbyManager.GetHobyByUser(user);
                }
                else
                {
                     work = workMnager.GetWorkByUser(getUserId);
                     education = educaionManager.GetEducationByUser(getUserId);
                     address = addressManager.GetAddressByUserId(getUserId);
                    hobby = hobbyManager.GetHobyByUser(getUserId);
                    

                }
            





                ViewBag.CoverPhoto = profile.CoverPicture;
                ViewBag.ProfilePicture = profile.Picture;
                ViewBag.Work = work;
                ViewBag.Education = education;
                ViewBag.Address = address;
                ViewBag.Hobby = hobby;

                ViewBag.ConfirmFriendRequest = friendRequestManager.ConfirmFriendRequest(user.Id,sentTORequest.Id);








                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;


                return View();
            }
            else
            {
                return View("Error");
            }
         
        }

        public ActionResult EditProfile()
        {
            //newsFeedController.Layout();

            var user = (User)Session["User"];
            


            if (login.CheckSession(user))
            {
           

                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
              //get parent information
              var parent = parentManger.GetParentsById(user);
            
                
                //get 
                profile = profileManager.GetProfileByUserId(user.Id);

               
                ViewBag.FirstName =profile.FristName;
                ViewBag.LastName = profile.LastName;
                ViewBag.MotherName = parent.MotherName;
                ViewBag.FatherName = parent.FatherName;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                
                user.Profile = profile;
                user.Parents = parent;
                Session["User"] = user;


                return View();
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AddressEdit()
        {
            

            var user = (User)Session["User"];

       


            if (login.CheckSession(user))
            {
                var address = addressManager.GetAddressByUserId(user);
                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;


                user.Address = address;
                ViewBag.City = address.City;
                ViewBag.HomeTown = address.Hometown;
                ViewBag.Country = address.Country;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;

                return View();
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public RedirectToRouteResult Address(ViewModelAddress viewModelAddress)
        {



            var user = (User)Session["User"];

            Address address =  new Address();
            address.City = viewModelAddress.City;
            address.Country = viewModelAddress.Country;
            address.Hometown = viewModelAddress.HomeTown;
            addressManager.MakeADisitionUpdateOrSave(address, user);
            return RedirectToAction("AddressEdit");
        }

      

        public ActionResult WorkInfoEdit()
        {
            
            var user = (User)Session["User"];

            


            if (login.CheckSession(user))
            {

                var work = workMnager.GetWorkByUser(user);


                ViewBag.Company = work.Company;
                ViewBag.City = work.City;
                ViewBag.Position = work.Position;
                ViewBag.Description = work.Description;

                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                user.Work = work;
                Session["User"] = user;
                return View();
            }
            else
            {
                return View("Error");
            }
        }


        [HttpPost]
        public RedirectToRouteResult WorkInfo(ViewModelWork viewModelWork)
        {

            var user = (User)Session["User"];
            Work work = new Work();

            work.Company = viewModelWork.Company;
            work.Position = viewModelWork.Position;
            work.City = viewModelWork.City;
            work.Description = viewModelWork.Description;

            workMnager.MakeADecisionSaveOrUpdate(work,user);







            return RedirectToAction("WorkInfoEdit");
        }

        public ActionResult AreaOfInterrestEdit()
        {

            
            var user = (User)Session["User"];
           

            if (login.CheckSession(user))
            {
                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
              var hobby=  hobbyManager.GetHobyByUser(user);



                ViewBag.Hobby = hobby.UserHobby;
                user.Hobby = hobby;
                Session["User"] = user;

                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return View();
            }
            else
            {
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return View("Error");
            }
        }


        [HttpPost]
        public RedirectToRouteResult AreaOfInterest(ViewModelHoby viewmodelHoby)
        {

           var user = (User) Session["User"];
            var hobby = new Hobby()
            {
                UserHobby = viewmodelHoby.Hobby
            };

            hobbyManager.MakeADecisionSaveOrUpdate(hobby,user);

                        
            return RedirectToAction("AreaOfInterrestEdit");
        }
        
        ProfileManager profileMnager =  new ProfileManager();
        public ActionResult ProfileEdit()
        {
           

            var user = (User)Session["User"];

            // get data from profile 
            
           


            if (login.CheckSession(user))
            {
                var profile = profileManager.GetProfileByUserId(user.Id);

                ViewBag.Image = profile.Picture;

                ViewBag.CurrentUserProfile = profile;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return View();
            }
            else
            {
                return View("Error");
            }
        }
        PostManager postManager = new PostManager();
        [HttpPost]
        public RedirectToRouteResult ImageUpload(ViewProfilePicture viewModelProfilePicture)
        {
            
            var user = (User)Session["User"];

            string messege = user.Profile.FristName +"  " +user.Profile.LastName + "Update Profile Picture";
            Post post =  new Post();
            
            string path = "";
            var profilePic = viewModelProfilePicture.Picture;



            if (profilePic != null)
            {
                string pic = System.IO.Path.GetFileName(profilePic.FileName);
                path = System.IO.Path.Combine(
                    Server.MapPath("~/Image/ProfilePic"), pic);
                // file is uploaded
                profilePic.SaveAs(path);
                post.PostPicture = viewModelProfilePicture.Picture.FileName;
                post.UserPost = messege;
            }
            else
            {
                post.PostPicture = "a";
                post.UserPost = messege;
            }

            
            postManager.MakeAPost(post, user.Id);
            //get path 
            var newuser =  new User();
            newuser = user;
            newuser.Profile.Picture = viewModelProfilePicture.Picture.FileName;
            //save path to  database 
            updatePicture.UpdateProfilePic(newuser);
            return RedirectToAction("ProfileEdit");
        }

        public ActionResult UpdateCoverPhoto(ViewModelCoverPicture viewmodelCoverPicture)
        {
            //newsFeedController.Layout();
            var user = (User)Session["User"];

            string messege = user.Profile.FristName + "  " + user.Profile.LastName + "Update Cover Picture";
            Post post = new Post();

            string path = "";
            var profilePic = viewmodelCoverPicture.Picture;
            
            if (profilePic != null)
            {
                string pic = System.IO.Path.GetFileName(profilePic.FileName);
                path = System.IO.Path.Combine(
                    Server.MapPath("~/Image/ProfilePic"), pic);
                // file is uploaded
                profilePic.SaveAs(path);
                post.PostPicture = viewmodelCoverPicture.Picture.FileName;
                post.UserPost = messege;
            }
            else
            {
                post.PostPicture = "a";
                post.UserPost = messege;
            }
            postManager.MakeAPost(post, user.Id);
            //get path 
            var newuser =  new User();
            newuser = user;
            newuser.Profile.Picture = viewmodelCoverPicture.Picture.FileName;           
            //save path to  database 
            updatePicture.UpdateCoverPic(newuser,newuser.Profile);
            return RedirectToAction("CoverPicture");




        }   


         


   

        public ActionResult ChangePassword()
        {
            //newsFeedController.Layout();
            var user = (User)Session["User"];
           


            if (login.CheckSession(user))
            {
                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return View();
            }
            else
            {
                return View("Error");
            }
        }
        PasswordManger passwordManger = new PasswordManger();
        [HttpPost]
        
        public RedirectToRouteResult ChanagePasswordEdit(ViewModelChangePassword changePassword)
        {

            var user = (User)Session["User"];
            user.Password = changePassword.Password;

            passwordManger.ChangeUserPassword(user);
   

            return RedirectToAction("ChangePassword");
        }

        public ActionResult Education()
        {
            var user = (User)Session["User"];

            
            if (login.CheckSession(user))
            {

                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
                var education = educaionManager.GetEducationByUser(user);

                ViewBag.School = education.School;
                ViewBag.College = education.College;
                ViewBag.University = education.University;

                user.Education = education;

                Session["User"] = user;

                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public RedirectToRouteResult EducationEdit(ViewModelEducation viewModelEducation)
        {

            Education education = new Education()
            {
                School =  viewModelEducation.School,
                College =  viewModelEducation.College,
                University =  viewModelEducation.University
            };
            var user = (User) Session["User"];
              


            educaionManager.MakeADecisionSaveOrUpdate(education,user);          


            return RedirectToAction("Education");
        }

        public ActionResult CoverPicture()
        {            
            var user = (User)Session["User"];
            if (login.CheckSession(user))
            {

                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.CurrentUserProfile = profile;
                
                
                
                ViewBag.CoverPicture = profile.CoverPicture; 
                ViewBag.Name = user.Profile.FristName + " " + user.Profile.LastName;

                return View("CoverPhotoEdit");
            }
            else
            {
                return View("Error");
            }
        }
      
      

      
        [HttpPost]

        public RedirectToRouteResult Genarel(ViewModelGenarel genarelInfo)
        {

            var user = new User();
            var user2 = (User)Session["User"];
            
            ViewBag.Name = user2.Profile.FristName + " " + user2.Profile.LastName;
            
            //parent info
            user.Parents.MotherName = genarelInfo.MotherName;
            user.Parents.FatherName = genarelInfo.FatherName;

            //profile info
            user.Profile.FristName = genarelInfo.FristName;
            user.Profile.LastName = genarelInfo.LastName;
            user.Profile.NickName = genarelInfo.NickName;

            //update profile
            profileManager.UpdateProfile(user2, user.Profile);
            //save parent
            parentManger.MakeDesitionUpdateorSave(user.Parents,user2);

           



            return RedirectToAction("EditProfile");
        }
        public ActionResult AboutView()
        {



            var user = (User)Session["User"];

            

            if (login.CheckSession(user))
            {

                user.Profile = profileManager.GetProfileByUserId(user.Id);
                user.Address = addressManager.GetAddressByUserId(user);
                user.Education = educaionManager.GetEducationByUser(user);
                user.Hobby = hobbyManager.GetHobyByUser(user);
                ViewBag.User = user;

                var profile = profileManager.GetProfileByUserId(user.Id);
                ViewBag.Profile = profile;
                return View();
            }else
            {

                return View("Error");
 
            }

            //repeted code 
            
        }



       
	}
}
