using MediatR;
using TestCQRS.Dtos;

namespace TestCQRS.Instructors.Command
{
    public record AddInstructorCommand(AddInstructorDto addInstructor) : IRequest<bool>;
}
