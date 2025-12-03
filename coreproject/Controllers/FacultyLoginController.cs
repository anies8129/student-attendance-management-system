using coreproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class FacultyLoginController : Controller
    {
        FacultyLoginDb obj = new FacultyLoginDb();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Logincls clsobj)
        {
            string result = obj.LoginDb(clsobj);

            if (result == "invalid")
            {
                ViewBag.msg = "Invalid Faculty Username or Password";
                return View();
            }

            if (!result.Contains("|"))
            {
                ViewBag.msg = "Unexpected login response: " + result;
                return View();
            }

            string[] data = result.Split('|');
            string logtype = data[0];
            string facultyId = data[1];


            HttpContext.Session.SetString("FacultyId", facultyId);
            HttpContext.Session.SetString("UserType", logtype);

            return RedirectToAction("Dashboard", "FacultyLogin");
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("FacultyId") == null)
            {
                return RedirectToAction("Login", "FacultyLogin");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "FacultyLogin");

        }

    }
}