using obs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obs.Controllers
{
    [Authorize]

    public class ÖğrenciController : Controller
    {

        private sporEntities db = new sporEntities();
        public ActionResult Index()
        {
            
                var students = db.@student.ToList();
                return View(students);
            
        }
        public ActionResult Register()
        {
            var classList = db.@class
                .Select(c => new
                {
                    cid = c.cid,
                    DisplayName = c.classname + " " + c.classyear
                })
                .ToList();

            ViewBag.ClassId = new SelectList(classList, "cid", "DisplayName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(student student, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    string extension = Path.GetExtension(imageFile.FileName);

                    if (!allowedExtensions.Contains(extension.ToLower()))
                    {
                        ViewBag.ErrorMessage = "Geçersiz dosya uzantısı. Lütfen .jpg, .jpeg, .png veya .gif dosyaları yükleyin.";
                        ViewBag.ClassId = new SelectList(db.@class, "cid", "DisplayName", student.clid);
                        return View(student);
                    }

                    using (var binaryReader = new BinaryReader(imageFile.InputStream))
                    {
                        student.studentphoto = binaryReader.ReadBytes(imageFile.ContentLength);
                    }
                }

                db.student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(student);
        }
    }
}
