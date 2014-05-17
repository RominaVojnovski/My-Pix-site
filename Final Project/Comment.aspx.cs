using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Final_Project.Models;
using Microsoft.AspNet.Identity;
using System.Web.UI.HtmlControls;

namespace Final_Project
{
    public partial class Comment1 : System.Web.UI.Page
    {
        int cid;
        String commentID;
        Comment mycomment;

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
                           //create new div row
                            
                            HtmlGenericControl divrow = new HtmlGenericControl("div");
                            divrow.Attributes.Add("class", "row");

                            HtmlGenericControl divcol12 = new HtmlGenericControl("div");
                            divcol12.Attributes.Add("class", "col-md-12");

                            Label newLabel = new Label();
                            newLabel.ID = "NewLabel" + r;
                            newLabel.Text = x.ResponseText;
                         
                            
                            Label newLabel2 = new Label();
                            newLabel2.ID = "NewLabel_2"+ r;
                            newLabel2.Text = Convert.ToString(x.WhenPosted)+" EST";

                            Label newLabel3 = new Label();
                            newLabel3.ID = "NewLabel_3" + r;
                            newLabel3.Text = "by: " + x.Poster.PosterName + "&nbsp;&nbsp;";

                            divcol12.Controls.Add(newLabel);
                            divcol12.Controls.Add(new LiteralControl("<br/>"));
                            divcol12.Controls.Add(newLabel3);
                            
                            divcol12.Controls.Add(newLabel2);
                            divcol12.Controls.Add(new LiteralControl("<hr />"));
                            divrow.Controls.Add(divcol12);

                            PlaceHolderDynamicDiv.Controls.Add(divrow);
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
            string uid = User.Identity.GetUserId();
            
            
            using (MyContext db2 = new MyContext())
            {
                /*make sure user is a poster*/
                Poster findUser = (from p in db2.Posters
                                   where p.PosterId == uid
                                   select p).FirstOrDefault();
                /*if user is not found in Posters table this means he/she has not confirmed*/
                if (findUser == null)
                {
                    OutputLabel.Text = "Please check your email and confirm your registration first!";
                    return;
                }
                
                
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