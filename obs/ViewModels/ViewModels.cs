using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace obs.ViewModels
{
    public class AttendanceReportViewModel
    {
        public DateTime Date { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<StudentAttendance> StudentAttendances { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }
    }

    public class StudentAttendance
    {
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public bool IsPresent { get; set; }
    }
}
