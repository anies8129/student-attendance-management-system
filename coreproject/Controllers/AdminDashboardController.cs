using Microsoft.AspNetCore.Mvc;

namespace coreproject.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AdminId") == null)
            {
                return RedirectToAction("Login", "AdminLogin");
            }

            return View();

        }
    }
}
