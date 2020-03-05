using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.WebUI.Models
{
    public class HomeModel
    {
        public List<Blog> Sliders { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
