using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Instructors.Query
{
    public record GetPointInstructorQuery : IRequest<IEnumerable<PointInstructorDto>>;
    
}
