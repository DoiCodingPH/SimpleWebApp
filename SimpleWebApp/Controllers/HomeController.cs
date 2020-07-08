using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //localhost:54329/
            //localhost:54329/Home/Index
            return View();
        }

        public IActionResult Post()
        {
            //localhost:54329/Home/Post
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Post());
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            return RedirectToAction(nameof(this.Post));
        }
    }
}
