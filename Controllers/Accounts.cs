using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web31.Controllers
{
    public class Accounts : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
