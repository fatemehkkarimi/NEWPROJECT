using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using newproject.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using newproject.Models.Models;
using newproject.Models.Context;
using newproject.Models.DataBase;
using Microsoft.AspNetCore.Hosting;
namespace newproject.Controllers
{
    [Area("Admin")]
    public class AdminAboutUsController : Controller
    {
        private readonly Application db;
        private readonly IWebHostEnvironment en;

        public AdminAboutUsController( Application _db, IWebHostEnvironment _en)
        {
            db = _db;
            en = _en;
        }  
        public IActionResult Show(string id)
        {
          var find = db.tbl_AboutUS.OrderByDescending(p=>p.Id).ToList();
          if (find != null)
          {
            ViewBag.AboutUs = find;
          }
          if (id != null)
          {
            ViewBag.Message = id;
          }
            return View();
        }
        public IActionResult gotoupdate(int id)
        {
            var find = db.tbl_AboutUS.SingleOrDefault(p=>p.Id == id);
            if (find != null)
            {
                Vm_AboutUS h = new Vm_AboutUS();
                h.Vm_Id = find.Id;
                h.Vm_Title = find.Title;
                h.Vm_Detail = find.Detail;
                return View(h);
            }else
            {
                return RedirectToAction("Show", new {id = "مورد انتخابی یافت نشد"});
            }
        }
        public IActionResult update(Vm_AboutUS k)
        {
           if (k != null)
           {
            var find = db.tbl_AboutUS.SingleOrDefault(p=>p.Id == k.Vm_Id);
            find.Title = k.Vm_Title;
            find.Detail = k.Vm_Detail;
            db.tbl_AboutUS.Update(find);
            db.SaveChanges();
            return RedirectToAction("Show", new {id = "عملیات ویرایش باموفقیت انجام شد"});
           }else
           {
            return RedirectToAction("Show", new {id = "فیلد های ورودی را چک نمایید"});
           }
        }
        
    }   
}



