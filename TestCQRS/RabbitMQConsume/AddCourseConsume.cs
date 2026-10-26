using MediatR;
using TestCQRS.Courses.Command;
using TestCQRS.RabbitMQMessages;

namespace TestCQRS.RabbitMQConsume
{
    public class AddCourseConsume : IConsume<AddCourseMessage>
    {
        private readonly IMediator _mediator;

        public AddCourseConsume(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(AddCourseMessage message)
        {
            var command = new AddCourseCommand(
                message.Name,
                message.Hours,
                message.InstructorId
            );

          await _mediator.Send(command);
        }
    }
}
