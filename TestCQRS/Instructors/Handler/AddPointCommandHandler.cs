using MediatR;
using TestCQRS.Instructors.Command;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Instructors.Handler
{
    public class AddPointCommandHandler : IRequestHandler<AddPointCommand, bool>
    {
        private readonly IReposirory<PointInstructor> _instRepository;

        public AddPointCommandHandler(IReposirory<PointInstructor> InstRepository)
        {
            _instRepository = InstRepository;
        }
        public async Task<bool> Handle(AddPointCommand request, CancellationToken cancellationToken)
        {

            await _instRepository.Add(new PointInstructor() { InstructorId = request.InstId, Point = request.point });

            await _instRepository.SaveChanges();

            return true;
        }
    }
}
