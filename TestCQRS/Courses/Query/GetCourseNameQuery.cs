using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Courses.Query
{
    public record GetCourseNameQuery(string name): IRequest<List<CourseDto>>;
    
}
