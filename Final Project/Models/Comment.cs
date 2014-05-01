using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string Title { get; set; }
        public String CommentText { get; set; }
        public virtual Poster Poster { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Response> Responses { get; set; }



    }
}