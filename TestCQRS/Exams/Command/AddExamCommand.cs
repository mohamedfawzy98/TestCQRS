using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Exams.Command
{
    public record AddExamCommand(AddExamDto examDto) : IRequest<bool>;
   
}
