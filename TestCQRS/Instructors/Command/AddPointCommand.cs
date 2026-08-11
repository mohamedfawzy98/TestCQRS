using MediatR;

namespace TestCQRS.Instructors.Command
{
    public record AddPointCommand(int point,int InstId) : IRequest<bool>;
    
}
