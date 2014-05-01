using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Final_Project.Models;
using System.Web.ModelBinding;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace Final_Project
{
    public partial class Tags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            using (MyContext context = new MyContext())
            {
                var all_tags = from tag in context.Tags
                               select new { ID = tag.TagsId, Text = tag.TagText };


                string uid = User.Identity.GetUserId();


            }
        }
    }
}