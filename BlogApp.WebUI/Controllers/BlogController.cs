using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;

namespace BlogApp.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogReporsitory _blogReporsitory;
        private ICategoryRepository _categoryRepository;
        public BlogController(IBlogReporsitory blogrepo, ICategoryRepository categoryrepo)
        {
            _blogReporsitory = blogrepo;
            _categoryRepository = categoryrepo;
        }
        public IActionResult Index(int? id,string q)
        {
            var query = _blogReporsitory.GetAll().Where(i => i.isApproved);

            if (id != null)
            {
                query = query.Where(i => i.isApproved && i.CategoryId == id);
            }
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(i => i.Title.Contains(q) || i.Description.Contains(q) || i.Body.Contains(q));
            }

            return View(query.OrderByDescending(i => i.Date));
        }

        public IActionResult Details(int id)
        {
            return View(_blogReporsitory.GetByID(id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            CategoryGetAll();
            return View(new Blog());
        }

        [HttpPost]
        public IActionResult Create(Blog entity)
        {
            entity.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _blogReporsitory.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} güncellenmiştr.";
                return RedirectToAction("List");
            }
            CategoryGetAll();
            return View(entity);
        }

        public IActionResult List()
        {
            return View(_blogReporsitory.GetAll());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CategoryGetAll();
            return View(_blogReporsitory.GetByID(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        entity.Image = file.FileName;
                    }
                }
                _blogReporsitory.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} güncellendi.";
                return RedirectToAction("List");
            }
            CategoryGetAll();
            return View(entity);
        }
        [HttpGet]
        //public IActionResult AddOrUpdate(int? id)
        //{
        //    if (id == null)
        //    {
        //        CategoryGetAll();
        //        return View();
        //    }
        //    else
        //    {
        //        CategoryGetAll();
        //        return View(_blogReporsitory.GetByID((int)id));
        //    }
        //}

        //[HttpPost]
        //public IActionResult AddOrUpdate(Blog entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _blogReporsitory.SaveBlog(entity);
        //        TempData["message"] = $"{entity.Title} güncellenmiştr.";
        //        return RedirectToAction("List");
        //    }
        //    CategoryGetAll();
        //    return View(entity);
        //}

        public void CategoryGetAll()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "Id", "Name");
        }

        public IActionResult Delete(int id)
        {
            return View(_blogReporsitory.GetByID(id));
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogReporsitory.DeleteBlog(id);
            return RedirectToAction("List");
        }
    }
}