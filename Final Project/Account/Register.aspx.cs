using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Final_Project.Models;
using System.IO;
using System.Net.Mail;



namespace Final_Project.Account
{
    public partial class Register : Page
    {

        private string CreateConfirmationToken()
        {
            return ShortGuid.NewGuid();
            
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            string confirmationToken = CreateConfirmationToken();
            var manager = new UserManager();
            var user = new ApplicationUser() {usersName=users_Name.Text, UserName = UserName.Text, email_address = Email_Address.Text, ConfirmationToken = confirmationToken, IsConfirmed = false, WhenRegistered= DateTime.Now, WhenConfirmed=DateTime.Now, IsAda=AdaCheckBox.Checked, UserProfileText=UserProfileText.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            
            if (result.Succeeded)
            {


                string newUserId = user.Id;

                String urlBase = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
                String verifyUrl = "VerifyNewUser.aspx?ID=" + newUserId;
                String fullPath = urlBase + verifyUrl;
                String AppPath = Request.PhysicalApplicationPath;
                StreamReader sr = new StreamReader(AppPath + "App_Data/SignUpConfirmation.txt");

                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("romina.vojnovska@gmail.com");
                message.To.Add(new MailAddress(user.email_address));
                message.CC.Add(new MailAddress("romina.vojnovska@gmail.com"));
                message.Subject = "New User Registration!";

                message.Body = sr.ReadToEnd();
                sr.Close();

                message.Body = message.Body.Replace("<%UserName%>", UserName.Text);
                message.Body = message.Body.Replace("<%Password%>", Password.Text);
                message.Body = message.Body.Replace("<%VerificationUrl%>", fullPath);

                SmtpClient client = new SmtpClient();
                client.Send(message);

                
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                Response.Redirect("~/Complete.aspx");
                /**IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);**/
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}