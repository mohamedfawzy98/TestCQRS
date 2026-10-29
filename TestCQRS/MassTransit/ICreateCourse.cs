namespace TestCQRS.MassTransit
{
    public interface ICreateCourse
    {
        public string Name { get; }
        public int Hours { get; }
        public int InstructorId { get; }
    }
}
