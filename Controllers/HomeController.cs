using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NEWPROJECT.Models;
using newproject.Models.Context;

namespace NEWPROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
         private readonly Application db;

        public HomeController(ILogger<HomeController> logger, Application _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Index()
        {
           var header = db.tbl_Headers.ToList();
           if (header != null)
           {
            ViewBag.Header = header;
           }
           var companys = db.tbl_Companies.Where(p=>p.Status == true).ToList();
           if (companys != null)
           {
            ViewBag.Company = companys;
           }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
