using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.AspNet.Identity;
using Final_Project.Models;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Final_Project
{
    public partial class Upload : System.Web.UI.Page
    {

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

           


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (FileUploadPhoto.HasFile)
            {
                
                string uid = User.Identity.GetUserId();

                /*make sure user is a poster*/
                using (MyContext db = new MyContext())
                {
                    Poster findUser = (from p in db.Posters
                                   where p.PosterId == uid
                                   select p).FirstOrDefault();
                    /*if user is not found in Posters table this means he/she has not confirmed*/
                    if (findUser==null)
                    {
                    OutputLabel.Text = "Please check your email and confirm your registration first!";
                    return;
                    }
                    
                };

                /*Get extension of the uploaded file*/
                String ext = Path.GetExtension(FileUploadPhoto.PostedFile.FileName).ToLower();
                
                /*validate the file for file type of gif jpg or png only*/
                if (ext.Equals(".jpg") || ext.Equals(".gif") || ext.Equals(".png")) {
                    /*...and size*/
                    if (FileUploadPhoto.PostedFile.ContentLength > 53000000)
                    {
                        OutputLabel.Text = "The file size limit is 50MB";
                        return;

                    }
                    /*upload the file to the respective users image folder*/
                    var serverPhotosFolder = Server.MapPath("~/User_Photos/"+User.Identity.GetUserName());
                    /*FileUploadPhoto.SaveAs(Path.Combine(serverPhotosFolder, FileUploadPhoto.FileName));*/
                    /*resize the file*/
                    /*create bitmap from uploaded image*/
                    Bitmap bmpUploadedImage = new Bitmap(FileUploadPhoto.PostedFile.InputStream);
                    
                    Bitmap newImage = new Bitmap(500, 400);
                    using (Graphics gr = Graphics.FromImage(newImage))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(bmpUploadedImage, new Rectangle(0, 0, 500, 400));
                    }

                    System.Drawing.Image newImg = newImage;
                    newImg.Save(Path.Combine(serverPhotosFolder, FileUploadPhoto.FileName));


                    string PhotoPath = Path.Combine(serverPhotosFolder, FileUploadPhoto.FileName);
                    /*update the Photos and Tags tables*/

                    using (MyContext db2 = new MyContext())
                    {
                        
                        Poster thisPoster = (from p in db2.Posters
                                            where p.PosterId == uid
                                            select p).FirstOrDefault();
                        
                        Photo myPhoto = new Photo();
                        
                        
                        myPhoto.WhenPosted = DateTime.Now;
                        myPhoto.Title = TitleTextBox.Text;
                        myPhoto.Poster = thisPoster;
                        myPhoto.PhotoPath = "~/User_Photos/"+User.Identity.GetUserName()+"/"+FileUploadPhoto.FileName;
                        myPhoto.Tags = new List<Models.Tags>();
                        myPhoto.Comments = new List<Models.Comment>();
                        
                        

                        /*if tags are specified create them in database*/
                        string tagtext1 = TagTextBox.Text;


                        if (tagtext1.Length > 0)
                        {
                            /*handle multiple tags if commas and remove whitespace*/
                            string lower = tagtext1.ToLower();
                            String[] tagsArray = lower.Split(',').Select(s => s.Trim()).ToArray();
                            int x = 0;
                            //list of already created tags in db
                            List<Models.Tags> checktags = db2.Tags.ToList();
                            bool flag;
                            Models.Tags newTag1;
                            while (x < tagsArray.Length)
                            {
                                /*but first check to see if that tag already exists*/
                                flag = false;
                                foreach (var t in checktags)
                                {
                                    /*if the tag entered is equal to a tag in the tag db then skip to the next entered tag*/
                                    if ((tagsArray[x]).Equals(t.TagText))
                                    {
                                        flag = true;
                                        /*newTag1 = new Models.Tags
                                        {
                                            TagText = tagsArray[x],
                                            Photos = new List<Photo>(),
                                        };
                                        */
                                        t.Photos.Add(myPhoto);
                                        myPhoto.Tags.Add(t);
                                        
                                    }
                                }

                                if (flag == false)
                                {
                                    newTag1 = new Models.Tags
                                    {
                                        TagText = tagsArray[x],
                                        Photos = new List<Photo>(),
                                    };

                                    newTag1.Photos.Add(myPhoto);
                                    db2.Tags.Add(newTag1);
                                    myPhoto.Tags.Add(newTag1);
                                   
                                }

                                x++;

                            }/*end of tag housekeeping WHILE LOOP*/

                        }//if tags were specified in textbox
                        
                            db2.Photos.Add(myPhoto);
                        
                            db2.SaveChanges();
                    
                        
                        };
                    
                        OutputLabel.Text = "Upload successful!";
                }
                else{
                    OutputLabel.Text = "Only .png, .jpg, or .gif file types accepted. You have: "+ext;
                    return;
                }
             
            }/*end of if FileUploadPhoto has file*/
            else
            {
                OutputLabel.Text = "You have not chosen a file to upload!";
            }
        }

       
    }
}