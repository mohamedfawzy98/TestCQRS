using MediatR;

namespace TestCQRS.Notfigation.Command
{
    public record AddNotfigationCommand(string? Title, string? Message, DateTime CreatedAt, int? CourseId) : IRequest<bool>;
    
}
