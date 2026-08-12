using MediatR;
using TestCQRS.Notfigation.Command;

namespace TestCQRS.BackGroundServices
{
    public class NotificationJob
    {
        private readonly IMediator _mediator;

        public NotificationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendNotification(AddNotfigationCommand command)
        {
            await _mediator.Send(command, CancellationToken.None);
        }
    }
}
