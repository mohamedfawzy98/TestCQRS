namespace TestCQRS.Dtos
{
    public class AddExamDto
    {
        public string ExamName { get; set; } = null!;
        public int Degree { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
    }
}
