using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;
using Microsoft.AspNet.Identity;
using System.Data;

namespace Final_Project
{
    public partial class Photos : System.Web.UI.Page
    {
        String view;
        
        
        public string photoPoster(string uid)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == uid);

            return user.UserName;
        }

        void Page_PreInit(object sender, EventArgs e)
        {

            string uid = User.Identity.GetUserId();

            /*make sure there is user logged in*/
            using (ApplicationDbContext db1 = new ApplicationDbContext())
            {
                var findUser1 = (from u in db1.Users
                                 where u.Id == uid
                                 select u).FirstOrDefault();

                if (findUser1 != null)
                {
                    if (findUser1.IsAda)
                    {
                        Page.Theme = "ADA";
                    }
                }

            };

        }//end preinit

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }

            view = Request.QueryString["view"];
            /*if the user wants to see the list view*/
            if ((view == null) || (view == "") || (view == "list"))
            {

                using (MyContext db = new MyContext())
                {


                    var all_photos = from p in db.Photos
                                     orderby p.WhenPosted
                                     select p;




                    List<Photo> myList = new List<Photo>();
                    myList = all_photos.ToList();
                    string tagBuilder = "";
                    List<Models.Tags> taglist = new List<Models.Tags>();
                    //string output="";


                    /* Build prototype of empty list*/
                    var superList = Enumerable.Empty<object>().Select(r => new
                    {
                        Title = "",
                        PhotoID = 0,
                        Poster = "",
                        Photo = "",
                        Tags = ""
                    }).ToList();

                    foreach (Photo thisphoto in myList)
                    {
                        tagBuilder = "";
                        taglist = thisphoto.Tags.ToList();

                        foreach (var x in taglist)
                        {
                            tagBuilder += x.TagText + ";";
                        }


                        superList.Add(new
                        {
                            Title = thisphoto.Title,
                            PhotoID = thisphoto.PhotoId,
                            Poster = thisphoto.Poster.PosterName,
                            Photo = thisphoto.PhotoPath,
                            Tags = tagBuilder

                        });
                        /*output += tagBuilder;*/
                    }

                    //Label1.Text = output;




                    PhotosGridView.DataSource = superList;
                    PhotosGridView.DataBind();

                };
            }//end of if list view is chosen


            /*If the user has chose to view the gallery view*/
            else if (view == "gallery")
            {
                using (MyContext db2 = new MyContext())
                {
                    String temp = "";
                    var all_photos = from p in db2.Photos
                                     orderby p.WhenPosted
                                     select p;

                    List<Photo> myList2 = all_photos.ToList();


                    /* Build prototype of empty list*/
                    var superList2 = Enumerable.Empty<object>().Select(r => new
                    {
                        FullPhotoPath=""
                    }).ToList();

                    foreach (Photo p in myList2)
                    {
                        temp=p.PhotoPath.Replace("~/","");
                        superList2.Add(new { 
                            FullPhotoPath=temp
                        });

                    }

                    ListViewGal.DataSource = superList2;
                    ListViewGal.DataBind();
                };


            }

        }//page load end
    }
}