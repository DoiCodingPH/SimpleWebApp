using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Data;
using SimpleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AdminController(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }
        
        //Admin/Index or /Admin
        public IActionResult Index()
        {
            var posts = this._dbContext.Posts.ToList();
            return View(posts);
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

            return RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.Id == id);
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(this.Index));
        }
    }
}
