using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.ViewComponents
{
    public class MonthlySpecials : ViewComponent
    {
        private BlogDataContext _db;

        public MonthlySpecials(BlogDataContext db)
        {
            this._db = db;

        }
        public IViewComponentResult Invoke()
        {
            var specials = this._db.MonthlySpecials.ToArray();
            return View(specials);
        }
    }
}
