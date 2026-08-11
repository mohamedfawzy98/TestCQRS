using MediatR;
using TestCQRS.Courses.Command;
using TestCQRS.Instructors.Command;
using TestCQRS.Orchastretor.CourseOrch.Command;

namespace TestCQRS.Orchastretor.CourseOrch.Handler
{
    public class AddCourseOrchCommandHandler : IRequestHandler<AddCourseOrchCommand, bool>
    {
        IMediator _mediator;
        public AddCourseOrchCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(AddCourseOrchCommand request, CancellationToken cancellationToken)
        {
            // 1 - Add Course
            await _mediator.Send(new AddCourseCommand(request.name, request.hour, request.instid));

            // 2 - Add Point

            await _mediator.Send(new AddPointCommand(request.point, request.instid));

            return true;
        }
    }
}
