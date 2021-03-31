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
        public IActionResult Index(int page = 0)
        {
            double pageSize = 2d;
            double totalPosts = _db.Posts.Count();
            double pages = totalPosts / pageSize;
            int totalPages = (int)Math.Ceiling(pages);
            int previousPage = page - 1;
            int nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts = _db.Posts
                .OrderByDescending(x => x.Posted)
                .Skip((int)(pageSize * page))
                .Take((int)pageSize)
                .ToArray();

            if(Request.Headers["X-Requested-with"] == "XMLHttpRequest")
            {
                return PartialView(posts);
            }

            return View(posts);
        }

        [HttpGet("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = this._db.Posts.FirstOrDefault(p => p.Key == key);
            return PartialView(post);
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
