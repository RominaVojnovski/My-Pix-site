using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Final_Project.Models;
using Microsoft.AspNet.Identity;

namespace Final_Project
{
    public partial class Comment1 : System.Web.UI.Page
    {
        int cid;
        String commentID;
        Comment mycomment;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            commentID = Request.QueryString["ID"];
            if (commentID == null || commentID == "")
            {
                
                return;
            }
            else
            {
                cid = Convert.ToInt32(commentID);
                Photo commentedPhoto;
                using (MyContext db = new MyContext())
                {
                    /* grab the comment that matches the query string ID*/
                    mycomment = (from c in db.Comments
                                         where c.CommentId == cid
                                         select c).FirstOrDefault();

                    commentedPhoto = mycomment.Photo;
                    MainImage.ImageUrl = commentedPhoto.PhotoPath;
                    LabelCommentTitle.Text = mycomment.Title;
                    LabelCommentBody.Text = mycomment.CommentText;


                }; 
              
               
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*Add response to mycomment*/
            /*Similar to adding a comment to photo*/
            using (MyContext db2 = new MyContext())
            {
                cid = Convert.ToInt32(commentID);
                mycomment = (from c in db2.Comments
                             where cid == c.CommentId
                             select c).FirstOrDefault();

                string responderid = User.Identity.GetUserId();
                Poster responder = (from r in db2.Posters
                                    where r.PosterId == responderid
                                    select r).FirstOrDefault();

                Models.Response newresponse = new Models.Response();
                newresponse.WhenPosted = DateTime.Now;
                newresponse.ResponseText = TextBoxResponse.Text;
                responder.Responses.Add(newresponse);
                mycomment.Responses.Add(newresponse);
                db2.Responses.Add(newresponse);
                db2.SaveChanges();
          
            };
        }
    }
}