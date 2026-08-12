using MediatR;
using TestCQRS.Models;
using TestCQRS.Notfigation.Command;
using TestCQRS.Repositories;

namespace TestCQRS.Notfigation.Handler
{
    public class AddNotfigationCommandHandler : IRequestHandler<AddNotfigationCommand, bool>
    {
        private readonly IReposirory<Models.Notfigation> _notfigationRepo;

        public AddNotfigationCommandHandler(IReposirory<TestCQRS.Models.Notfigation> notfigationRepo)
        {
            _notfigationRepo = notfigationRepo;
        }
        public async Task<bool> Handle(AddNotfigationCommand request, CancellationToken cancellationToken)
        {
         await  _notfigationRepo.Add(new Models.Notfigation
            {
                Title = request.Title,
                Message = request.Message,
                CreatedAt = DateTime.Now,
                CourseId = request.CourseId
            });

            await _notfigationRepo.SaveChanges();
            return true;
        }
    }
}
