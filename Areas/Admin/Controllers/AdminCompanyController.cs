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
    public class AdminCompanyController : Controller
    {
        private readonly Application db;
        private readonly IWebHostEnvironment en;

        public AdminCompanyController( Application _db, IWebHostEnvironment _en)
        {
            db = _db;
            en = _en;
        }  
        public IActionResult show(string Id)
        {
           var find = db.tbl_Companies.ToList();
           if (find != null)
           {
            ViewBag.Company = find;
           }
           if (Id != null)
           {
            ViewBag.Message = Id;
           }
            return View();
        }
        public IActionResult gotoadd()
        {
            return View();
        }
        public async Task<IActionResult> add(Vm_Company k)
        {
          if (k != null)
          {
            Tbl_Company n = new Tbl_Company();
            n.Title = k.Vm_Title;
            n.Status = true;
            n.Image = "";
            
                if (k.Vm_Img != null)
                {
                    string FileExtension1 = Path.GetExtension(k.Vm_Img.FileName);
                    string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
                    var path = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await k.Vm_Img.CopyToAsync(stream);
                    }
                    n.Image =NewFileName;
                }
           db.tbl_Companies.Add(n);
           db.SaveChanges();
           return RedirectToAction("show" , new{Id = "عملیات افزودن شرکت با موفقیت انجام شد"});
        }else
        {
            return RedirectToAction("gotoadd" , new{Id = "لطفا پارامتر های ورودی را چک نمایید"});
        }
        }
        public IActionResult gotostatus(int id)
        {
            var find = db.tbl_Companies.SingleOrDefault(p=>p.Id == id);
            if (find != null)
            {
                if (find.Status == true)
                {
                    find.Status = false;
                }else
                {
                    find.Status = true;
                }
                db.tbl_Companies.Update(find);
                db.SaveChanges();
                return RedirectToAction("show" , new{Id = "عملیات تغییر وضعیت شرکت با موفقیت انجام شد"});
            }else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
            return View();
        }
        public IActionResult gotodelet(int id)
        {
           var find = db.tbl_Companies.SingleOrDefault(p=>p.Id == id);
           if (find != null)
           {
               string Pathdelet = $"{en.WebRootPath}\\fileupload\\{find.Image}";
                FileInfo files = new FileInfo(Pathdelet);
                if (files.Exists)
                {
                 files.Delete();
                }
               db.Remove(find);
               db.SaveChanges();
                return RedirectToAction("show" , new{Id = "عملیات حذف شرکت با موفقیت انجام شد"});
           }else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
        }
        public IActionResult gotoupdate(int id)
        {
            var find = db.tbl_Companies.SingleOrDefault(p=>p.Id == id);
            if (find != null)
            {
             Vm_Company k = new Vm_Company();
             k.Vm_Id = find.Id;
             k.Vm_Image = find.Image;
             k.Vm_Title = find.Title;
             return View(k);
            }
            else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
        }
        public async Task<IActionResult> update(Vm_Company k)
        {
            if (k != null)
            {
                var find = db.tbl_Companies.SingleOrDefault(p=>p.Id == k.Vm_Id);
                find.Title =  k.Vm_Title;
                if (k.Vm_Img != null)
                {
                    string Pathdelet = $"{en.WebRootPath}\\fileupload\\{find.Image}";
                    FileInfo files = new FileInfo(Pathdelet);
                    if (files.Exists)
                    {
                     files.Delete();
                    }
                    if (k.Vm_Img != null)
                    {
                        string FileExtension1 = Path.GetExtension(k.Vm_Img.FileName);
                        string NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
                        var path = $"{en.WebRootPath}\\fileupload\\{NewFileName}";
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await k.Vm_Img.CopyToAsync(stream);
                        }
                        find.Image =NewFileName;
                    }
                }
                    db.Update(find);
                    db.SaveChanges();
                    return RedirectToAction("show" , new{Id = "عملیات ویرایش شرکت با موفقیت انجام شد"});
            }
            else
            {
                return RedirectToAction("gotoupdate" , new{Id = "لطفا پارامتر های ورودی را چک نمایید"});
            }
     }
        
    }

  }



