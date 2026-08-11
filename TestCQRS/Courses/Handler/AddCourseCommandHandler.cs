using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCQRS.Courses.Command;
using TestCQRS.EventSource;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Courses.Handler
{
    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, bool>
    {
        private readonly IReposirory<Course> _courseRepo;
        private readonly IMediator _mediator;

        public AddCourseCommandHandler(IReposirory<Course> CourseRepo , IMediator mediator)
        {
            _courseRepo = CourseRepo;
            _mediator = mediator;
        }
        public async Task<bool> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            // Handle Logic

            // 1 - Mappig
            await _courseRepo.Add(new Course
            {
                Name = request.name,
                Hours = (int)request.Hours,
                InstructorId = request.InstId
            });
            await _courseRepo.SaveChanges();


            // Implement Event for Point Instructor

            await _mediator.Publish(new AddCourseEvent(request.name, request.Hours, request.InstId, 6));

            return true;


        }
    }
}
