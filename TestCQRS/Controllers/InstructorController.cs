using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCQRS.BackGroundServices;
using TestCQRS.Dtos;
using TestCQRS.Instructors.Command;
using TestCQRS.Notfigation.Command;
using TestCQRS.VM;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InstructorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddInstructor(AddInstructorVm addInstructor)
        {
            var Mapper = new AddInstructorDto
            {
                Name = addInstructor.Name
            };

            await _mediator.Send(new AddInstructorCommand(Mapper));
            // Fire And Forget Job
            var command = new AddNotfigationCommand(
                $"Notfigation Add Instructor {Mapper.Name}",
                "",
                DateTime.Now,
                1
            );

            BackgroundJob.Enqueue<NotificationJob>(job => job.SendNotification(command));
            return Ok("تم الحفظ بنجاح");
        }
    }
}
