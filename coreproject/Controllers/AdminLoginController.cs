using coreproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class AdminLoginController : Controller
    {
        AdminLoginDb obj = new AdminLoginDb();

        [HttpGet]
        public IActionResult Login()
        {
            // STEP 3 — Save AdminName in session (done after POST login)
            return View();
        }

        [HttpPost]
        public IActionResult Login(Logincls clsobj)
        {
            string result = obj.LoginDb(clsobj);

            if (result == "invalid")
            {
                ViewBag.msg = "Invalid username or password";
                return View();
            }

            if (!result.Contains("|"))
            {
                ViewBag.msg = "Unexpected login response: " + result;
                return View();
            }

            // Format MUST be: role | adminId | adminName
            string[] parts = result.Split('|');

            if (parts.Length != 3)
            {
                ViewBag.msg = "Login format error";
                return View();
            }

            string logtype = parts[0];     // "admin"
            string adminId = parts[1];     // e.g., "1"
            string adminName = parts[2];   // e.g., "Karthik"

            // Save to session
            HttpContext.Session.SetString("UserType", logtype);
            HttpContext.Session.SetString("AdminId", adminId);
            HttpContext.Session.SetString("AdminName", adminName);

            return RedirectToAction("Dashboard", "AdminDashboard");
        }
    }
}