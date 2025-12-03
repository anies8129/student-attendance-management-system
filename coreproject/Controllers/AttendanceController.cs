using coreproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class AttendanceController : Controller
    {
        AttendanceDb db = new AttendanceDb();

        // SHOW LIST OF STUDENTS
        public IActionResult MarkAttendanceList()
        {
            if (HttpContext.Session.GetString("FacultyId") == null)
                return RedirectToAction("Login", "FacultyLogin");

            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));
            var students = db.GetStudentsByFaculty(facultyId);

            return View(students);
        }

        // MARK PRESENT
        public IActionResult MarkPresent(int id)
        {
            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));
            db.QuickMarkAttendance(id, facultyId, "Present");
            return RedirectToAction("MarkAttendanceList");
        }

        // MARK ABSENT
        public IActionResult MarkAbsent(int id)
        {
            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));
            db.QuickMarkAttendance(id, facultyId, "Absent");
            return RedirectToAction("MarkAttendanceList");
        }
        public IActionResult ViewAttendance()
        {
            if (HttpContext.Session.GetString("FacultyId") == null)
            {
                return RedirectToAction("Login", "FacultyLogin");
            }

            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));

            var list = db.GetAttendanceByFaculty(facultyId);

            return View(list);
        }
    }
}

