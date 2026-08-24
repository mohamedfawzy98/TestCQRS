namespace TestCQRS.Dtos
{
    public class GetCoursesDTO
    {
        public string Name { get; set; } = null!;
        public int Hours { get; set; }
        public string InstructorName { get; set; } = null!;
    }
}
