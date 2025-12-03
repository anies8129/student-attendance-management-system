using Microsoft.AspNetCore.Mvc;
using coreproject.Models;

namespace coreproject.Controllers
{
    public class AdminController : Controller
    {
       AdminRepository obj=new AdminRepository();
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Admin clsobj)
        {
            string msg = obj.InsertAdmin(clsobj);
            ViewBag.message = msg;

            return View();
        }
    }
}

