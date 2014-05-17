using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Final_Project.Models;

namespace Final_Project.Account
{
    public partial class Login : Page
    {
        
        void Page_PreInit(object sender, EventArgs e)
        {
         
            if (Request.Cookies["UserSettings"] != null)
            {
                string userSettings;
                if (Request.Cookies["UserSettings"]["ADA"] != null)
                {
                    userSettings = Request.Cookies["UserSettings"]["ADA"];

                    if (userSettings == "True")
                    {
                        Page.Theme = "ADA";
                    }
                }
            }
          

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
            RegisterHyperLink.NavigateUrl = "Register";
            
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                if (user != null)
                {
               
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);

                   

                    /*create cookie for ADA*/
                    HttpCookie myCookie = new HttpCookie("UserSettings");
                    
                    
                    
                    using (ApplicationDbContext db1 = new ApplicationDbContext())
                    {
                        var findUser1 = (from u in db1.Users
                                         where u.Id == user.Id
                                         select u).FirstOrDefault();

                        if (findUser1 != null)
                        {
                            if (findUser1.IsAda)
                            {
                                myCookie["ADA"] = "True";
                               
                            }
                            else
                            {
                                myCookie["ADA"] = "False";
                            }

                            Response.Cookies.Add(myCookie);
                        
                        }

                    };
                    
              
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}