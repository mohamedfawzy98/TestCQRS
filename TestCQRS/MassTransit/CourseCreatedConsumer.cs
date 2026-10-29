using MassTransit;
using MediatR;
using TestCQRS.Courses.Command;

namespace TestCQRS.MassTransit
{
    public class CourseCreatedConsumer : IConsumer<ICreateCourse>
    {
        private readonly IMediator _mediator;

        public CourseCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<ICreateCourse> context)
        {
            var command = new AddCourseCommand(
                context.Message.Name,
                context.Message.Hours,
                context.Message.InstructorId
            );

            await _mediator.Send(command);
        }
    }
}
