using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.WebUI.Controllers
{
    
    public class HomeController : Controller
    {
        private IBlogReporsitory blogReporsitory;
        public HomeController(IBlogReporsitory _blogReporsitory)
        {
            blogReporsitory = _blogReporsitory;
        }
        public IActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.Blogs = blogReporsitory.GetAll().Where(i => i.isHome && i.isApproved).ToList();
            model.Sliders = blogReporsitory.GetAll().Where(i => i.isHome && i.isApproved && i.isSlider).ToList();
            return View(model);
        }
        public IActionResult List()
        {
            return View();
        }
     
        public IActionResult Details()
        {
            return View();
        }
    }
}