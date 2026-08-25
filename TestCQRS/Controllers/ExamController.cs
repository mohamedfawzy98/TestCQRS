using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestCQRS.BackGroundServices;
using TestCQRS.Dtos;
using TestCQRS.Exams.Command;
using TestCQRS.Notfigation.Command;
using TestCQRS.VM;

namespace TestCQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamController(IMediator  mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddExam")]
       public async Task<bool> AddExam(AddExamVm addExam)
        {
            var mapExamVm = new AddExamDto
            {
                ExamName = addExam.ExamName,
                Degree = addExam.Degree,
                CourseId = addExam.CourseId,
                InstructorId = addExam.InstructorId
            };
            await _mediator.Send(new AddExamCommand(mapExamVm));

            // Fire And Forget Job
            var command = new AddNotfigationCommand(
                $"Add Exam  {mapExamVm.ExamName}",
                "",
                DateTime.Now,
                1
            );

            BackgroundJob.Enqueue<NotificationJob>(job => job.SendNotification(command));
            return true;
        }
    }
}
