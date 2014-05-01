using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class Photo
    {
        
        public int PhotoId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }

        public virtual Poster Poster { get; set; }

        public virtual ICollection<Tags> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }



        
       
    }
}