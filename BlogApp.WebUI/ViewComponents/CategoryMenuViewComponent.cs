 using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.ViewComponents
{
    public class CategoryMenuViewComponent:ViewComponent
    {
        private ICategoryRepository _repository;
        public CategoryMenuViewComponent(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCAtegory = RouteData.Values["id"];
            return View(_repository.GetAll());
        }

    }
}
