using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;


namespace Final_Project
{
    public partial class Photo1 : System.Web.UI.Page
    {
        int pid;
        Photo photo1;
        String imgID;

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

            imgID = Request.QueryString["ID"];
            if (imgID == null || imgID == "")
            {
                //Label1.Text = "Oops that image does not exist!";
                return;
            }
            using (MyContext db = new MyContext())
            {

                /*using ID retrieve the photo to be displayed*/
                pid = Convert.ToInt32(imgID);
                photo1 = (from p in db.Photos
                          where p.PhotoId == pid
                          select p).FirstOrDefault();

                /*if no valid photo queried*/
                if (photo1 == null)
                {
                    //Label1.Text = "Ooops that image does not exist!";
                    return;
                }
                /*valid photo being queried*/
                else
                {

                    MainImage.ImageUrl = photo1.PhotoPath;
                    ImageTitle.Text = photo1.Title;
                    DateLabel.Text = Convert.ToString(photo1.WhenPosted);
                    PosterLabel.Text = photo1.Poster.PosterName;
                    string tagBuilder = "";
                    List<Models.Tags> taglist = new List<Models.Tags>();
                    taglist = photo1.Tags.ToList();

                    //build the string of tags for the photo
                    foreach (var x in taglist)
                    {
                        tagBuilder += x.TagText + ";";
                    }
                    TagsLabel.Text = tagBuilder;

                    /*populate gridview with the commens for photo*/
                    List<Comment> commentList = new List<Comment>();
                    commentList = photo1.Comments.ToList();

                    if (commentList != null)
                    {

                        /* Build prototype of empty list*/
                        var superList = Enumerable.Empty<object>().Select(r => new
                        {
                            CommentID = 0,
                            CommentSubtext = "",
                            ResponsesCount = 0
                        }).ToList();

                        /*parse thru each comment in the photo's comment list*/
                        /* for each comment add it to superlist row of commentsubtext and count 2 colums*/
                        int responsesnum = 0;
                        string preview = "";
                        List<Response> responseList = new List<Response>();
                        foreach (var x in commentList)
                        {
                            responsesnum = 0;
                            if (x.CommentText.Length < 15)
                            {
                                preview = x.CommentText;
                            }
                            else
                            {
                                //preview = x.CommentText.Substring(0, 50) + "...";
                                preview = truncateString(x.CommentText, 15);
                            
                            
                            
                            }

                            /*count how many responsesnum for each comment*/
                            responseList = x.Responses.ToList();

                            //if there are responses associated with the comment
                            if (responseList != null)
                            {
                                foreach (var y in responseList)
                                {
                                    responsesnum++;

                                }
                            }



                            superList.Add(new
                            {
                                CommentID = x.CommentId,
                                CommentSubtext = preview,
                                ResponsesCount = responsesnum

                            });

                        }//foreach comment in the list

                        GridViewComments.DataSource = superList;
                        GridViewComments.DataBind();

                    }//end if block to handle comments

                }//end else (found valid photo)
            };//end MyContext scope   
        }

        
        /*truncate custom method*/
        public static string truncateString(string originalString, int length)
        {
            if (string.IsNullOrEmpty(originalString))
            {
                return originalString;
            }
            if (originalString.Length > length)
            {
                return originalString.Substring(0, length) + "...";
            }
            else
            {
                return originalString;
            }
        }
        
        
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string uid = User.Identity.GetUserId();

            /*add comment to photo*/

            using (MyContext cdb = new MyContext())
            {


                /*make sure user is a poster*/
                Poster findUser = (from p in cdb.Posters
                                   where p.PosterId == uid
                                   select p).FirstOrDefault();
                /*if user is not found in Posters table this means he/she has not confirmed*/
                if (findUser == null)
                {
                    OutputLabel.Text = "Please check your email and confirm your registration first!";
                    return;
                }
                
                pid = Convert.ToInt32(imgID);
                photo1 = (from p in cdb.Photos
                          where p.PhotoId == pid
                          select p).FirstOrDefault();

                string commenterid = User.Identity.GetUserId();
                Poster commenter = (from c in cdb.Posters
                                    where c.PosterId == commenterid
                                    select c).FirstOrDefault();


                Comment new_comment = new Comment();
                new_comment.Title = CommentTitle.Text;
                new_comment.CommentText = CommentText.Text;
                new_comment.WhenPosted = DateTime.Now;
                photo1.Comments.Add(new_comment);
                commenter.Comments.Add(new_comment);
                new_comment.Responses = new List<Response>();
                cdb.Comments.Add(new_comment);
                cdb.SaveChanges();
            };
            OutputLabel.Text = "Your comment has been submitted!";
            Response.Redirect("Photo.aspx?ID=" + imgID);
        }
    }
}
