using Microsoft.AspNetCore.Mvc;
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
    }
}
