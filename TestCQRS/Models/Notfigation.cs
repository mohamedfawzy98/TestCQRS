namespace TestCQRS.Models
{
    public class Notfigation : BaseModel
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
