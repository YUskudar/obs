using obs.Models;
using obs.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace obs.Controllers
{
    [Authorize]

    public class SınıfController : Controller
    {
        private sporEntities db = new sporEntities();

        public ActionResult Index()
        {
            var classes = db.@class.ToList();
            return View(classes);
        }

        public ActionResult StudentsByClass(int id)
        {
            var students = db.student.Where(s => s.clid == id).ToList();

            return View(students);
        }


        public ActionResult TakeAttendance(int classId)
        {
            ViewBag.ClassId = classId;
            ViewBag.Students = db.student.Where(s => s.clid == classId).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TakeAttendance(int classId, DateTime attendanceDate, int[] studentIds)
        {
            if (studentIds != null)
            {
                foreach (var studentId in studentIds)
                {
                    var attendance = new attandce
                    {
                        classid = classId,
                        studentid = studentId,
                        adate = attendanceDate
                    };
                    db.attandce.Add(attendance);
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult AttendanceReport()
        {
            var classes = db.@class.ToList();
            var classSelectList = classes.Select(c => new
            {
                cid = c.cid,
                DisplayName = c.classname + " " + c.classyear
            });

            var model = new AttendanceReportViewModel
            {
                Classes = new SelectList(classSelectList, "cid", "DisplayName")
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AttendanceReport(AttendanceReportViewModel model)
        {
            var students = db.student.Where(s => s.clid == model.ClassId).ToList();
            var attendances = db.attandce
                .Where(a => a.classid == model.ClassId && DbFunctions.TruncateTime(a.adate) == model.Date.Date)
                .ToList();

            model.ClassName = db.@class.Find(model.ClassId)?.classname;
            model.StudentAttendances = students.Select(s => new StudentAttendance
            {
                StudentName = s.studentname,
                StudentSurname = s.studentsurname,
                IsPresent = attendances.Any(a => a.studentid == s.sid)
            }).ToList();

            model.Classes = new SelectList(db.@class.ToList(), "cid", "classname", model.ClassId);

            return View(model);
        }


    }
}
