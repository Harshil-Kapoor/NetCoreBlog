using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    [Route("Home")]
    public class HomeController
    {
        [HttpGet("Index")]
        public string Index()
        {
            return "Index action";
        }
    }
}
