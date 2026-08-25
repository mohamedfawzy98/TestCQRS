using MediatR;
using Microsoft.EntityFrameworkCore;
using TestCQRS.Dtos;
using TestCQRS.Instructors.Query;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Instructors.Handler
{
    public class GetPointInstructorQueryHandler : IRequestHandler<GetPointInstructorQuery, IEnumerable<PointInstructorDto>>
    {
        private readonly IReposirory<PointInstructor> _reposirory;

        public GetPointInstructorQueryHandler(IReposirory<PointInstructor> reposirory)
        {
            _reposirory = reposirory;
        }
        public async Task<IEnumerable<PointInstructorDto>> Handle(GetPointInstructorQuery request, CancellationToken cancellationToken)
        {
            var GetAllPoints = await _reposirory.GetAll().Select(x => new PointInstructorDto()
            {
                NameInst = x.Instructor != null ? x.Instructor.Name : "Unknown",
                Points = x.Point
            }).GroupBy(x => x.NameInst).Select(g => new PointInstructorDto()
            {
                NameInst = g.Key,
                Points = g.Sum(x => x.Points)
            }).ToListAsync();

            return GetAllPoints;
        }

    }
}
