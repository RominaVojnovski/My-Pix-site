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
                    
                    Bitmap newImage = new Bitmap(400, 300);
                    using (Graphics gr = Graphics.FromImage(newImage))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(bmpUploadedImage, new Rectangle(0, 0, 400, 300));
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
                        if (tagtext1.Length>0)
                        {

                            Models.Tags newTag1 = new Models.Tags();
                            newTag1.TagText = tagtext1;
                            newTag1.Photos = new List<Photo>();
                            newTag1.Photos.Add(myPhoto);
                            db2.Tags.Add(newTag1);
                            myPhoto.Tags.Add(newTag1);
                            
                        }/*end of tag housekeeping*/
                        db2.Photos.Add(myPhoto);
                        db2.SaveChanges();
                    };
                    OutputLabel.Text = "File uploaded successfully!";
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