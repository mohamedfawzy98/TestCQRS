using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestCQRS.BackGroundServices;
using TestCQRS.Courses.Command;
using TestCQRS.Courses.Query;
using TestCQRS.Dtos;
using TestCQRS.Notfigation.Command;
using TestCQRS.Orchastretor.CourseOrch.Command;
using TestCQRS.Services;
using TestCQRS.VM;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RabbitMQServices _rabbitMQServices;
        public CourseController(IMediator mediator, RabbitMQServices rabbitMQServices)
        {
            _mediator = mediator;
            _rabbitMQServices = rabbitMQServices;
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(string name, int Hours, int InstId, int Point)
        {
            //  await _mediator.Send(new AddCourseCommand(name, Hours, InstId));

            // Orch

            //await _mediator.Send(new AddCourseOrchCommand(name, Hours, InstId, Point));

            // EventSourse
            await _mediator.Send(new AddCourseCommand(name, Hours, InstId));

            // Fire And Forget Job
            var command = new AddNotfigationCommand(
                "Notfigation Add Course",
                $"{name} Course is Added Now",
                DateTime.Now,
                1
            );

            BackgroundJob.Enqueue<NotificationJob>(job => job.SendNotification(command));
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

        [HttpGet("GetAll")]
        public async Task<IEnumerable<GetCourseVM>> GetAllCourse()
        {
            var Result = await _mediator.Send(new GetAllQuery());
            return Result.Select(c => new GetCourseVM
            {
                Name = c.Name,
                Hours = c.Hours,
                InstructorName = c.InstructorName
            });
        }


        // Test RabbitMQ
        [HttpPost("SendMessage")]
        public IActionResult SendMessage(CourseDto course)
        {
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(course);
            _rabbitMQServices.PublishMessage(message);

            return Ok("Message sent to RabbitMQ successfully.");
        }
    }
}
