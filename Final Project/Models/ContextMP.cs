using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class ContextMP:DbContext
    {
        public ContextMP() : base("MPData") { }


        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tags> Tags { get; set; }

    }
}