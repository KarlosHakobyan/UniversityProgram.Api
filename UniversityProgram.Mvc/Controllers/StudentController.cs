using Microsoft.AspNetCore.Mvc;

namespace UniversityProgram.Mvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View("New");
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
