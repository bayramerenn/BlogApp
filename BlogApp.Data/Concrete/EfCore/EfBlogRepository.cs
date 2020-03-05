using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfBlogRepository : IBlogReporsitory
    {
        private BlogContext context;
        public EfBlogRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = GetByID(blogId);
            if (blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs;
        }

        public Blog GetByID(int blogId)
        {
            return context.Blogs.Where(i => i.Id == blogId).FirstOrDefault();
        }

        public void SaveBlog(Blog entity)
        {
            if (entity.Id == 0)
            {
                entity.Date = DateTime.Now;
                context.Blogs.Add(entity);
            }
            else
            {
                var blog = GetByID(entity.Id);
                if (blog != null)
                {
                    blog.Title = entity.Title;
                    blog.Description = entity.Description;
                    blog.CategoryId = entity.CategoryId;
                    blog.Image = entity.Image;
                    blog.isApproved = entity.isApproved;
                    blog.isHome = entity.isHome;
                    blog.isSlider = entity.isSlider;
                    blog.Body = entity.Body;
                    
                }
            }
            context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
            var blog = GetByID(entity.Id);
            if (blog != null)
            {
                blog.Title = entity.Title;
                blog.Description = entity.Description;
                blog.CategoryId = entity.CategoryId;
                blog.Image = entity.Image;
                context.SaveChanges();
            }
           
        }

    }
}
