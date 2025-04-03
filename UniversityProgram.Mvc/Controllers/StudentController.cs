using Microsoft.AspNetCore.Mvc;

namespace UniversityProgram.Mvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewData["StudentName"] = "Aram";
            ViewData["StudentAge"] = 28;
            return View();
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
