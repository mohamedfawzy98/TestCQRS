using MediatR;

namespace TestCQRS.EventSource
{
    public record AddCourseEvent(string name , int hours , int InstId , int Point) : INotification;
    
}
