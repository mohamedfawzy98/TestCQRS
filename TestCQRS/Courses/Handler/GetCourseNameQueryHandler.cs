using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCQRS.Courses.Query;
using TestCQRS.Dtos;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Courses.Handler
{
    public class GetCourseNameQueryHandler : IRequestHandler<GetCourseNameQuery, List<CourseDto>>
    {
        private readonly IReposirory<Course> _courseRepository;

        public GetCourseNameQueryHandler(IReposirory<Course> CourseRepository)
        {
            _courseRepository = CourseRepository;
        }
        public async Task<List<CourseDto>> Handle(GetCourseNameQuery request, CancellationToken cancellationToken)
        {
           var Result = await _courseRepository.Get(x => x.Name.Contains(request.name))
                 .Select(x => new CourseDto(x.Name,x.Hours,x.InstructorId)).ToListAsync();

            return Result;
        }
    }
}
