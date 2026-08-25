using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCQRS.Courses.Query;
using TestCQRS.Dtos;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Courses.Handler
{
    public class GetAllQueryCommand : IRequestHandler<GetAllQuery, IEnumerable<GetCoursesDTO>>
    {
        private readonly IReposirory<Course> _reposirory;

        public GetAllQueryCommand(IReposirory<Course> reposirory)
        {
           _reposirory = reposirory;
        }
        public async Task<IEnumerable<GetCoursesDTO>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
         var courses = await _reposirory.GetAll().
          Select(c => new GetCoursesDTO
         {
             Name = c.Name,
             Hours = c.Hours,
             InstructorName = c.Instructor != null ? c.Instructor.Name : "No Instructor"
          }).ToListAsync(cancellationToken);

            return courses;
        }
    }
}
