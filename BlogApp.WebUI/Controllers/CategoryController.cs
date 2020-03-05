using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IBlogReporsitory _blogRepository;
        public CategoryController(ICategoryRepository categoryRepository, IBlogReporsitory blogReporsitory)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogReporsitory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            return View(_categoryRepository.GetAll());
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Category entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _categoryRepository.AddCategory(entity);
        //        return RedirectToAction("List");
        //    }

        //    return View(entity);
        //}

        [HttpGet]
        public IActionResult AddOrUpdate(int? id)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(_categoryRepository.GetById((int)id));

            }
        }

        [HttpPost]
        public IActionResult AddOrUpdate(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.SaveCategory(category);

                TempData["message"] = $"{category.Name} güncellenmiştir.";
                return RedirectToAction("List");
            }


            return View(category);


        }



    }
}