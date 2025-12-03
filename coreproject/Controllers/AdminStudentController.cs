using Microsoft.AspNetCore.Mvc;
using coreproject.Models;

namespace coreproject.Controllers
{
    public class AdminStudentController : Controller
    {
        AdminStudentDb db = new AdminStudentDb();

        public IActionResult Index()
        {
            var students = db.GetAllStudents();
            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Studentcls obj)
        {
            ViewBag.msg = db.InsertStudent(obj);
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = db.GetAllStudents()
                            .FirstOrDefault(s => s.StudentId == id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Studentcls obj)
        {
            ViewBag.msg = db.UpdateStudent(obj);
            return View(obj);
        }

        public IActionResult Delete(int id)
        {
            db.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
