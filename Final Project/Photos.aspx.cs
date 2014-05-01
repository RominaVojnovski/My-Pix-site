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
        public string photoPoster(string uid)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == uid);

            return user.UserName;
        }
        
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            /*List<SampleUserClass> list = new List<SampleUserClass>();*/
            using(MyContext db = new MyContext()){

                var all_photos = from p in db.Photos
                                   orderby p.WhenPosted
                                   select p;

                

                List<Photo> myList = new List<Photo>();
                myList = all_photos.ToList();
                string tagBuilder = "";
                List<Models.Tags> taglist = new List<Models.Tags>();
                string output="";
                
                /* Build prototype of empty list*/
                var superList = Enumerable.Empty<object>().Select(r => new
                {
                    Title = "",
                    PhotoID = 0,
                    Poster = "", 
                    Photo = "",
                    Tags = ""



                }).ToList();






                foreach(Photo thisphoto in myList){
                    tagBuilder = "";
                    taglist=thisphoto.Tags.ToList();
                    
                    foreach (var x in taglist)
                    
                    {
                        tagBuilder += x.TagText + ";";
                    }


                    superList.Add(new
                    {
                        
                        Title = thisphoto.Title,
                        PhotoID= thisphoto.PhotoId,
                        Poster = thisphoto.Poster.PosterName,
                        Photo = thisphoto.PhotoPath,
                        Tags = tagBuilder

                    });
                    /*output += tagBuilder;*/
                }


                Label1.Text = output;
  
                PhotosGridView.DataSource = superList;
                PhotosGridView.DataBind();
            
            
            
            };
            
            /**populate image column with thumbnail of photo*/
            

        }
    }
}