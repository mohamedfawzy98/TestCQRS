using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestCQRS.Courses.Command;
using TestCQRS.Courses.Query;
using TestCQRS.Dtos;
using TestCQRS.Orchastretor.CourseOrch.Command;
using TestCQRS.VM;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, int Hours, int InstId, int Point)
        {
            //  await _mediator.Send(new AddCourseCommand(name, Hours, InstId));

            // Orch

            //await _mediator.Send(new AddCourseOrchCommand(name, Hours, InstId, Point));

            // EventSourse
            await _mediator.Send(new AddCourseCommand(name, Hours, InstId));


            return Ok("تم الحفظ بنجاح");
        }

        [HttpGet]
        public async Task<IEnumerable<CourseVM>> GetCourse(string name)
        {
            var Result = await _mediator.Send(new GetCourseNameQuery(name));

            var ResultVm = Result.Select(c => new CourseVM
            {
                InstructorId = c.InstructorId,
                Hours = c.Hours,
                Name = c.Name
            });

            return ResultVm;
        }
    }
}
