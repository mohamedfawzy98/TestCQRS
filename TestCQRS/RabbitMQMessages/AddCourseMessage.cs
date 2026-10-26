namespace TestCQRS.RabbitMQMessages
{
    public class AddCourseMessage : BaseMassege
    {
        public string Name { get; set; } = null!;
        public int Hours { get; set; }
        public int InstructorId { get; set; }

    }
}
