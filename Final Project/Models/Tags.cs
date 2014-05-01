using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class Tags
    {
        public int TagsId { get; set; }
        public String TagText { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        

    }
}