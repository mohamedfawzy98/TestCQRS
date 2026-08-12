using Microsoft.EntityFrameworkCore;
using TestCQRS.Models;

namespace TestCQRS.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
                
        }
       
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public  DbSet<PointInstructor> pointInstructors { get; set; }
        public DbSet<TestCQRS.Models.Notfigation>   Notfigations { get; set; }
    }
}
