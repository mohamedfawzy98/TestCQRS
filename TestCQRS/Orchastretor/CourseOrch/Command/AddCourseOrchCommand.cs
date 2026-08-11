using MediatR;

namespace TestCQRS.Orchastretor.CourseOrch.Command
{
    public record AddCourseOrchCommand(string name , int hour , int instid , int point) : IRequest<bool>;
    
}
