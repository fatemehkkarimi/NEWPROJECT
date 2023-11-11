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
    public class AdminHeaderController : Controller
    {
        private readonly ILogger<AdminHeaderController> _logger;
        private readonly Application db;
        private readonly IWebHostEnvironment en;

        public AdminHeaderController(ILogger<AdminHeaderController> logger, Application _db, IWebHostEnvironment _en)
        {
            _logger = logger;
            db = _db;
            en = _en;
        }

        public IActionResult Show(string Id)
        {
            var find = db.tbl_Headers.ToList();
            if (find != null)
            {
                ViewBag.Find = find;
            }
            if (Id != null)
            {
                ViewBag.Message = Id;
            }
            return View();
        }
        public IActionResult GoToUpdate(int id)
        {
              var find = db.tbl_Headers.SingleOrDefault(p=>p.Id == id);
              if (find != null)
              {
                Vm_Header n = new Vm_Header();
                n.Vm_Id = find.Id;
                n.Vm_Title = find.Title;
                n.Vm_Detail = find.Detail;
                n.Vm_Image = find.Image;
                return View(n);
              }
            return RedirectToAction("Show" , new{Id = "مورد انتخابی وجود ندارد"});
        }
        public async Task<IActionResult> Update(Vm_Header k)
        {
            if (k != null)
            {
              var find = db.tbl_Headers.SingleOrDefault(p=>p.Id == k.Vm_Id);
            find.Title = k.Vm_Title;
            find.Detail = k.Vm_Detail;
            if (k.Img != null)
            {
                string Pathdelet = $"{en.WebRootPath}\\fileupload\\{find.Image}";
                FileInfo files = new FileInfo(Pathdelet);
                if (files.Exists)
                {
                 files.Delete();
                }
                if (k.Img != null)
                {
                    string FileExtension1 = Path.GetExtension(k.Img.FileName);
                    string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
                    var path = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await k.Img.CopyToAsync(stream);
                    }
                    find.Image =NewFileName;
                }
            }
            db.tbl_Headers.Update(find);
            db.SaveChanges();
            }
             return RedirectToAction("Show" , new{Id = "عملیات ویرایش با موفقیت انجام شد"});
        }
      
    }
}
