using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class Response
    {
        public int ResponseId { get; set; }
        public DateTime WhenPosted { get; set; }
        public string ResponseText { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Poster Poster { get; set; }




    }
}