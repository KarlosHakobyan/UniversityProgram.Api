using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProgram.BLL.Services.StudentServices;

namespace NewApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestingController : ControllerBase
    {
        [HttpGet("Test")]
        public async Task<IActionResult> Test([FromServices]IStudentService studentService,CancellationToken token)
        {
            var students = await studentService.GetAll(token);
            return Ok(students);
        }
    }
}