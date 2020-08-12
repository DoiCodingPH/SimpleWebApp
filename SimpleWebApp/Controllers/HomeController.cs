using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Data;
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
        public async Task<IActionResult> Create(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(this.Post));
        }
    }
}
