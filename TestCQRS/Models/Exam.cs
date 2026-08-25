namespace TestCQRS.Models
{
    public class Exam : BaseModel
    {
        public string ExamName { get; set; } = null!;
        public int Degree { get; set; }
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public Course? Course { get; set; }
        public Instructor? Instructor { get; set; }

    }
}
