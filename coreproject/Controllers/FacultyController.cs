using coreproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class FacultyController : Controller
    {
        FacultyDb db = new FacultyDb();

        [HttpGet]
        public IActionResult AddFaculty()
        {
            if (HttpContext.Session.GetString("AdminId") == null)
                return RedirectToAction("Login", "AdminLogin");

            return View();
        }

        [HttpPost]
        public IActionResult AddFaculty(Facultycls clsobj)
        {
            string msg = db.InsertFaculty(clsobj);
            ViewBag.msg = msg;
            return View();
        }
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("FacultyId") == null)
                return RedirectToAction("Login", "FacultyLogin");

            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));

            FacultyDb db = new FacultyDb();
            FacultyProfile profile = db.GetFacultyProfile(facultyId);

            return View(profile);
        }
        public IActionResult EditProfile()
        {
            if (HttpContext.Session.GetString("FacultyId") == null)
                return RedirectToAction("Login", "FacultyLogin");

            int facultyId = Convert.ToInt32(HttpContext.Session.GetString("FacultyId"));

            FacultyDb db = new FacultyDb();
            FacultyProfile profile = db.GetFacultyProfile(facultyId);

            return View(profile);
        }

        [HttpPost]
        public IActionResult EditProfile(FacultyProfile obj)
        {
            FacultyDb db = new FacultyDb();
            string msg = db.UpdateFacultyProfile(obj);

            ViewBag.msg = msg;

            return View(obj);
        }
    }
}

