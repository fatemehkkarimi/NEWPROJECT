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
    public class AdminAboutUsItemController : Controller
    {
        private readonly Application db;
        private readonly IWebHostEnvironment en;

        public AdminAboutUsItemController( Application _db, IWebHostEnvironment _en)
        {
            db = _db;
            en = _en;
        }  
        public IActionResult show(string Id)
        {
           var find = db.tbl_AboutUSItems.ToList();
           if (find != null)
           {
            ViewBag.AboutUsItem = find;
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
        public IActionResult add(Vm_AboutUSItem k)
        {
          if (k != null)
          {
            if (!db.tbl_AboutUSItems.Any(p=>p.Number == k.Vm_Number))
            {
                Tbl_AboutUSItem n = new Tbl_AboutUSItem();
            n.Title = k.Vm_Title;
            n.Detail = k.Vm_Detail;
            n.Icon = k.Vm_Icon;
            n.Number = k.Vm_Number;
            n.Status = true;
           db.tbl_AboutUSItems.Add(n);
           db.SaveChanges();
           return RedirectToAction("show" , new{Id = "عملیات افزودن  با موفقیت انجام شد"});
            }else
            {
              return RedirectToAction("show" , new{Id = "شماره ایتم انتخاب شده موجود میباشد"});  
            }
        }else
        {
            return RedirectToAction("gotoadd" , new{Id = "لطفا پارامتر های ورودی را چک نمایید"});
        }
        }
        public IActionResult gotostatus(int id)
        {
            var find = db.tbl_AboutUSItems.SingleOrDefault(p=>p.Id == id);
            if (find != null)
            {
                if (find.Status == true)
                {
                    find.Status = false;
                }else
                {
                    find.Status = true;
                }
                db.tbl_AboutUSItems.Update(find);
                db.SaveChanges();
                return RedirectToAction("show" , new{Id = "عملیات تغییر وضعیت  با موفقیت انجام شد"});
            }else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
            return View();
        }
        public IActionResult gotodelet(int id)
        {
           var find = db.tbl_AboutUSItems.SingleOrDefault(p=>p.Id == id);
           if (find != null)
           {
               db.tbl_AboutUSItems.Remove(find);
               db.SaveChanges();
                return RedirectToAction("show" , new{Id = "عملیات حذف  با موفقیت انجام شد"});
           }else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
        }
        public IActionResult gotoupdate(int id)
        {
            var find = db.tbl_AboutUSItems.SingleOrDefault(p=>p.Id == id);
            if (find != null)
            {
             Vm_AboutUSItem k = new Vm_AboutUSItem();
             k.Vm_Id = find.Id;
             k.Vm_Title = find.Title;
             k.Vm_Detail = find.Detail;
             k.Vm_Icon = find.Icon;
             k.Vm_Number = find.Number;
             return View(k);
            }
            else
            {
                return RedirectToAction("show" , new{Id = "مورد انتخاب شده وجود ندارد"});
            }
        }
        public IActionResult update(Vm_AboutUSItem k)
        {
            if (k != null)
            {
                if (db.tbl_AboutUSItems.Any(p=>p.Number == k.Vm_Number))
            {
                var find = db.tbl_AboutUSItems.SingleOrDefault(p=>p.Id == k.Vm_Id);
                find.Title =  k.Vm_Title;
                find.Detail = k.Vm_Detail;
                find.Icon = k.Vm_Icon;
                find.Number = k.Vm_Number;
                db.tbl_AboutUSItems.Update(find);
                db.SaveChanges();
                return RedirectToAction("show" , new{Id = "عملیات ویرایش  با موفقیت انجام شد"});
            }else
            {
                return RedirectToAction("show" , new{Id = "شماره ایتم انتخاب شده موجود میباشد"}); 
            }
            } else
            {
                return RedirectToAction("gotoupdate" , new{Id = "لطفا پارامتر های ورودی را چک نمایید"});
            } 
        }
        
   }

}
