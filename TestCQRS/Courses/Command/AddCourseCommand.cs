using MediatR;

namespace TestCQRS.Courses.Command
{
    public record AddCourseCommand(string name , int Hours , int InstId) : IRequest<bool>;
}
