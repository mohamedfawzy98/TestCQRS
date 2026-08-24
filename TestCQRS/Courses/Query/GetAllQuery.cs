using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Courses.Query
{
    public record GetAllQuery : IRequest<IEnumerable<GetCoursesDTO>>;
    
}
