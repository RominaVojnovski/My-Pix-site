using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;
using Microsoft.AspNet.Identity;

namespace Final_Project
{
    public partial class Users : System.Web.UI.Page
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
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                
                /*create prototype of an empty list, see notes*/
                var superList = Enumerable.Empty<object>().Select(r => new
                {
                    Name = "",
                    Username = "",
                    Email = "",
                    Registered = "",
                    Confirmed = ""
                }).ToList();

                List <Final_Project.Models.ApplicationUser> allusers = (from appuser in context.Users
                                                                        orderby appuser.usersName
                                                                        select appuser).ToList();

                String na = "NA";
                
                foreach (Final_Project.Models.ApplicationUser thisuser in allusers)
                {

                    if (thisuser.WhenConfirmed == thisuser.WhenRegistered)
                    {
                        superList.Add(new
                        {
                            Name = thisuser.usersName,
                            Username = thisuser.UserName,
                            Email = thisuser.email_address,
                            Registered = thisuser.WhenRegistered.ToString(),
                            Confirmed = na
                        });

                    }
                    else
                    {
                        superList.Add(new
                        {
                            Name = thisuser.usersName,
                            Username = thisuser.UserName,
                            Email = thisuser.email_address,
                            Registered = thisuser.WhenRegistered.ToString(),
                            Confirmed = thisuser.WhenConfirmed.ToString()
                        });
                    }
                }


                GridView1.DataSource = superList;
                GridView1.DataBind();
                GridView1.Attributes.Remove("border");
              
            }

        }
    }
}