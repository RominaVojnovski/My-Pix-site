using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;

namespace Final_Project
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                
                /*create prototype of an empty list, see notes*/

                var u = from appuser in context.Users
                        orderby appuser.usersName
                        select new { Name=appuser.usersName, Username=appuser.UserName, Email=appuser.email_address, Registered=appuser.WhenRegistered, Confirmed=appuser.WhenConfirmed };

                GridView1.DataSource = u.ToList();
                GridView1.DataBind();
              
            }

        }
    }
}