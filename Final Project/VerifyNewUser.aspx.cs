using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Final_Project
{
    public partial class VerifyNewUser : System.Web.UI.Page
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
            if (string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                VerificationMessageLabel.Text = "No user id was specified...";

            }
            else
            {
                String userId;
                try
                {
                    userId = Request.QueryString["ID"];
                }
                catch
                {
                    VerificationMessageLabel.Text = "The user id specified is not in the proper format...";
                    return;
                }
                
                ApplicationDbContext context = new ApplicationDbContext();
                ApplicationUser user = context.Users.SingleOrDefault(u => u.Id == userId);
                
               
                if (user == null)
                {
                    VerificationMessageLabel.Text = "No corresponding user account found...";
                }
                else if (user.IsConfirmed == true)
                {
                    VerificationMessageLabel.Text = "The specified user account was previously activated...";
                }
                else
                {
                    user.IsConfirmed = true;
                    user.WhenConfirmed = DateTime.Now;
                    context.SaveChanges();

                    using (MyContext db = new MyContext()) {
                        var yesPoster = new Poster();
                        yesPoster.PosterId = userId;
                        yesPoster.PosterName = user.UserName;
                        db.Posters.Add(yesPoster);
                        db.SaveChanges();
                    }
                    /**create a directory in User_Photos for confirmed poster**/
                    using (ApplicationDbContext db2 = new ApplicationDbContext()) {
                        var getUser = (from u in db2.Users
                                       where u.Id == userId
                                       select u).FirstOrDefault();



                        string username = getUser.UserName;
                  
                        var rootFolder = Server.MapPath("~/User_Photos/"+username);
                   
                        Directory.CreateDirectory(rootFolder);


                   
                        VerificationMessageLabel.Text = username+", your account has been approved!";
                    }
                    
                    
                }
            }


        }
    }
}