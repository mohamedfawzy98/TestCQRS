namespace TestCQRS.Models
{
    public class PointInstructor : BaseModel
    {
        public int Point { get; set; }
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
