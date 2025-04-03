using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using UniversityProgram.Mvc.Models;

namespace UniversityProgram.Mvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewData["StudentName"] = "Aram";
            ViewData["StudentAge"] = 28;
            ViewBag.StudentName = "Vazgen";
            ViewBag.Title = 4;
            TempData["Student"] = "Samvel";
            var result = new List<UserViewModel>()
            {
                new UserViewModel()
                {
                   Name = "Poxos",
                   Age = 55,
                   ImageUrl = "https://images.unsplash.com/photo-1742201835840-1325b7546403?q=80&w=1965&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                new UserViewModel()
                    {
                   Name = "Nikoxos",
                   Age = 54,
                   ImageUrl ="https://images.unsplash.com/photo-1741732311526-093a69d005d9?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                 new UserViewModel()
                    {
                   Name = "Anna",
                   Age = 43,
                   ImageUrl ="https://images.unsplash.com/photo-1742218409598-e3cd0aa02145?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                },
                  new UserViewModel()
                    {
                   Name = "Vahe",
                   Age = 27,
                   ImageUrl =""
                }
            };

            return View(result);
        }

        public IActionResult New()
        {
            return View(8);
        }

        public IActionResult UserPage()
        {
            var user = new UserViewModel() { Name = "Gaspar", Age = 25 };
            return View("User", user);
        }
    }
}
