using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project.Models;
using System.Data.Entity;

namespace Final_Project.Models
{
    public class MyContext:DbContext
    {
        public MyContext() : base("MyPix") { }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Tags> Tags { get; set; }

        public DbSet<Poster> Posters { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}