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
            /*If user is not authenticated send them to login*/
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            /*set commentID from query string ID parameter*/
            commentID = Request.QueryString["ID"];

            /*if commentID is not valid give the user a message*/
            if (commentID == null || commentID == "")
            {
                
                return;
            }
            /*if commentID is valid proceed to show 
              photo, photo info, comment, and responses if any*/
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

                    List<Response> commentresponses = mycomment.Responses.ToList();
                    
                    /* If there are responses associated with comment...*/
                    if (commentresponses != null)
                    {
                        /*add each response text to the placeholder as a label*/
                        int r = 0;
                        foreach (var x in commentresponses)
                        {
                            Label newLabel = new Label();
                            newLabel.ID = "NewLabel" + r;
                            newLabel.Text = x.ResponseText;
                            PlaceHolderResponses.Controls.Add(newLabel);
                            PlaceHolderResponses.Controls.Add(new LiteralControl("<hr />"));

                            Label newLabel2 = new Label();
                            newLabel2.ID = "NewLabel_2"+ r;
                            newLabel2.Text = Convert.ToString(x.WhenPosted);
                            PlaceHolderDatePoster.Controls.Add(newLabel2);
                            PlaceHolderDatePoster.Controls.Add(new LiteralControl("<hr />"));

                            r++;

                        }

                    }

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
            Response.Redirect("Comment.aspx?ID=" + commentID);
        }
    }
}