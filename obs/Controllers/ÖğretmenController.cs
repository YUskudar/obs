using obs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obs.Controllers
{
    [Authorize]

    public class ÖğretmenController : Controller
    {
        private sporEntities db = new sporEntities();

        public ActionResult Index()
        {
            var teachers = db.teacher.ToList();
            return View(teachers);
        }
    }
}