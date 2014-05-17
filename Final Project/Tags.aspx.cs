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
            //getSelectedTagged();
            if (!IsPostBack)
            {
                bindRadioList();
            }
            
           
        }

        public void bindRadioList()
        {
            var optionList = new List<ListItem>();

            using (MyContext db = new MyContext())
            {
                var allTags = from t in db.Tags
                              select t;

                foreach (var x in allTags)
                {
                    RadioButtonListTags.Items.Add(new ListItem(x.TagText,x.TagsId.ToString()));
                }
            };

        }


        protected void RadioButtonListTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*event handler for radiobutton list when selected item changes*/
            /*gets the tag id of the selected tag*/
            int selectedTag = Convert.ToInt32(RadioButtonListTags.SelectedValue);

            /*retrieve the photos with associated tag id ~selectedValue*/
            
            setPhotosGrid(selectedTag);

        }

        public void setPhotosGrid(int tagID)
        {
            /* Build prototype of empty list*/
            var superList = Enumerable.Empty<object>().Select(r => new
            {
                Title = "",
                DatePosted = DateTime.Now,
                PhotoID = 0

            }).ToList();

            using (MyContext db2 = new MyContext())
            {
                var getPhotos = (from t in db2.Photos
                                 select t).ToList();
                /*for each photo...*/
                foreach (var p in getPhotos)
                {
                    /*retrieve the taglist for each photo*/
                    List<Models.Tags> taglist = p.Tags.ToList();
                    /*if taglist exists then check each tag to see if the tagid matches tagID*/
                    if (taglist != null)
                    {
                        foreach (Models.Tags t2 in taglist)
                        {
                            /*found a matching tag in the photo*/
                            if (t2.TagsId == tagID)
                            {
                               /*since found a matching tagID add this photo to superlist*/
                                superList.Add(new
                                {
                                    Title = p.Title,
                                    DatePosted = p.WhenPosted,
                                    PhotoID = p.PhotoId
                                });
                            }
                        }
                    }
                }
                if (superList != null)
                {
                    GridViewPhotosList.DataSource = superList;
                    GridViewPhotosList.DataBind();
                }
            
            
            };



        }

        protected void ButtonTag_Click(object sender, EventArgs e)
        {
            String submittedTags = TextBoxTag.Text;
            //split submitted tags textbox string into array split by comma
            String lower = submittedTags.ToLower();
            String[] tagsArray = lower.Split(',').Select(s=>s.Trim()).ToArray();
            int x = 0;
            
            Models.Tags addTag;
            bool flag;
             
                using (MyContext db3 = new MyContext())
                {
                    //parse thru tags and store each tag in db
                    List <Models.Tags>checktags = db3.Tags.ToList();
                   
                    while (x < tagsArray.Length)
                    { 
                        /*but first check to see if that tag already exists*/
                        flag = false;
                        foreach(var t in checktags){
                            /*if the tag entered is equal to a tag in the tag db then skip to the next entered tag*/
                            if ((tagsArray[x]).Equals(t.TagText))
                            {
                                flag = true;
                             
                            }  
                        }
                        
                        if (flag == false)
                        {
                            addTag = new Models.Tags
                            {
                                TagText = tagsArray[x]
                            };
                            db3.Tags.Add(addTag);
                        }
                       
                        x++;    
                    }
                    db3.SaveChanges();
                };
                
                Response.Redirect("Tags.aspx");
        }

       
    }
}