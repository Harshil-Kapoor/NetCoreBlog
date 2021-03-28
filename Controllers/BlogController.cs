using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly BlogDataContext _db;

        public BlogController(BlogDataContext db)
        {
            this._db = db;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var posts = this._db.Posts.OrderByDescending(p => p.Posted).Take(5).ToArray();
            return View(posts);
        }

        [HttpGet("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = this._db.Posts.FirstOrDefault(p => p.Key == key);
            return View(post);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            post.Author = User.Identity.Name;
            post.Posted = DateTime.Now;

            this._db.Add(post);
            this._db.SaveChanges();

            return RedirectToAction("Post", "Blog", new
                {
                    year = post.Posted.Year,
                    month = post.Posted.Month,
                    key = post.Key
                });
        }
    }
}
