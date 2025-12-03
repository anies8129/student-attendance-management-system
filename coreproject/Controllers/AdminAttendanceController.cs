using Microsoft.AspNetCore.Mvc;
using coreproject.Models;

namespace coreproject.Controllers
{
    public class AdminAttendanceController : Controller
    {
        AdminAttendanceDb db = new AdminAttendanceDb();

        [HttpGet]
        public IActionResult Index()
        {
            var data = db.FilterAttendance(null, null, null, null);
            return View(data);
        }

        [HttpPost]
        public IActionResult Index(int? studentid, int? facultyid, string department, DateTime? date)
        {
            var data = db.FilterAttendance(studentid, facultyid, department, date);
            return View(data);
        }
    }
}
