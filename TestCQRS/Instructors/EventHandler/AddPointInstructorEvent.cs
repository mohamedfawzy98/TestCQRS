using MediatR;
using TestCQRS.EventSource;
using TestCQRS.Instructors.Command;

namespace TestCQRS.Instructors.EventHandler
{
    public class AddPointInstructorEvent : INotificationHandler<AddCourseEvent>
    {
        private readonly IMediator _mediator;

        public AddPointInstructorEvent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(AddCourseEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddPointCommand(notification.Point, notification.InstId));
        }
    }
}
