using Microsoft.EntityFrameworkCore;
using System;
using BlogApp.Entity;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}
