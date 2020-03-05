using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Concrete.EfCore
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private BlogContext context;
        public EfCategoryRepository(BlogContext _context)
        {
            context = _context;
        }

        public void AddCategory(Category entity)
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }

        public void SaveCategory(Category entity)
        {
            if (entity.Id == 0)
            {
                context.Categories.Add(entity);
            }
            else
            {
                var categories = GetById(entity.Id);

                if (categories != null)
                {
                    categories.Name = entity.Name;
                }
            }
            context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = GetById(categoryId);
            if (category!=null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return context.Categories.FirstOrDefault(i => i.Id == categoryId);
        }

        public void UpdateCategory(Category entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
