using Microsoft.AspNetCore.Mvc;
using coreproject.Models;

namespace coreproject.Controllers
{
    public class AdminFacultyController : Controller
    {
        AdminFacultyDb db = new AdminFacultyDb();

        public IActionResult Index()
        {
            var data = db.GetAllFaculty();
            return View(data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(FacultyModel obj)
        {
            ViewBag.msg = db.InsertFaculty(obj);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var faculty = db.GetAllFaculty().FirstOrDefault(f => f.FacultyId == id);
            return View(faculty);
        }

        [HttpPost]
        public IActionResult Edit(FacultyModel obj)
        {
            ViewBag.msg = db.UpdateFaculty(obj);
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            db.DeleteFaculty(id);
            return RedirectToAction("Index");
        }
    }
}
