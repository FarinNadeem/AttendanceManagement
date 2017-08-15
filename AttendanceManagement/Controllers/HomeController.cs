using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Controllers
{
    public class HomeController : Controller
    {
        AttendanceManagementEntities db = new AttendanceManagementEntities();
        public ActionResult Index()
        {
            var students = db.Students.ToList();
            ViewBag.students = students;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int[] markStdId, DateTime AttDate)
        {
            int[] markAttendance = markStdId;
            var checkdate = db.Attendances.Where(c => c.Date == AttDate).FirstOrDefault();
            if (checkdate == null)
            {
                foreach (var Id in markAttendance)
                {
                    Attendance attendance = new Attendance();
                    attendance.StudentId = Id;
                    attendance.Date = AttDate;
                    db.Attendances.Add(attendance);
                    db.SaveChanges();
                    TempData["message"] = "Attendance for" + AttDate.ToString("d") + "has been marked";
                  
                }
            }

            else
            {
                TempData["messsage"] = "Attendance for" + AttDate.ToString("d") + "has already been marked";

            }

            var students = db.Students.ToList();
            ViewBag.students = students;

            return View();
        }
    }
}
