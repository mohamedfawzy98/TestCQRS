namespace TestCQRS.Models
{
    public class Course : BaseModel
    {
        public string Name { get; set; } = null!;
        public int Hours {  get; set; }
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
