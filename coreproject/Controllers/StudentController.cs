using coreproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class StudentController : Controller
    {
        StudentDb db=new StudentDb();
        [HttpGet]
        public IActionResult AddStudent()
        {
            if (HttpContext.Session.GetString("AdminId") == null)
            {
                return RedirectToAction("Login", "AdminLogin");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Studentcls clsobj)
        {
            string msg = db.InsertStudent(clsobj);
            ViewBag.msg = msg;

            return View();
        }
    }
}
