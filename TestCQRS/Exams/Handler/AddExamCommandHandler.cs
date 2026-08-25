using Hangfire.Storage.Monitoring;
using MediatR;
using TestCQRS.Dtos;
using TestCQRS.EventSource;
using TestCQRS.Exams.Command;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS.Exams.Handler
{
    public class AddExamCommandHandler : IRequestHandler<AddExamCommand, bool>
    {
        private readonly IReposirory<Exam> _reposirory;
        private readonly IMediator _mediator;

        public AddExamCommandHandler(IReposirory<Exam> reposirory, IMediator mediator)
        {
            _reposirory = reposirory;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AddExamCommand request, CancellationToken cancellationToken)
        {
            var MapExam = new Exam
            {
                ExamName = request.examDto.ExamName,
                Degree = request.examDto.Degree,
                CourseId = request.examDto.CourseId,
                InstructorId = request.examDto.InstructorId
            };

            await _reposirory.Add(MapExam);
            await _reposirory.SaveChanges();

            // fire event Add Points 

            await _mediator.Publish(new AddCourseEvent(request.examDto.ExamName, 2, request.examDto.InstructorId, 10));

            return true;
        }
    }
}
