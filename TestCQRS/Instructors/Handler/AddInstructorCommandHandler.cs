using MediatR;
using TestCQRS.EventSource;
using TestCQRS.Instructors.Command;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Instructors.Handler
{
    public class AddInstructorCommandHandler : IRequestHandler<AddInstructorCommand, bool>
    {
        private readonly IReposirory<Instructor> _reposirory;
        private readonly IMediator _mediator;

        public AddInstructorCommandHandler(IReposirory<Instructor> reposirory, IMediator mediator)
        {
            _reposirory = reposirory;
            _mediator = mediator;
        }
        public async Task<bool> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var Inst = new Instructor() { Name = request.addInstructor.Name };
            await _reposirory.Add(Inst);
            await _reposirory.SaveChanges();

            // fire Event Add Point

            await _mediator.Publish(new AddCourseEvent(request.addInstructor.Name, 0, Inst.Id, 5));

            return true;
        }
    }
}
