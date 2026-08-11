
using Microsoft.EntityFrameworkCore;
using TestCQRS.Contexts;
using TestCQRS.Models;
using TestCQRS.Repositories;

namespace TestCQRS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Defualt");

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IReposirory<Course>, Reposirory<Course>>();
            builder.Services.AddScoped<IReposirory<PointInstructor>, Reposirory<PointInstructor>>();

            // Registers all Handlers, Behaviors, and Prerequests from the Assembly where 'Program' is defined
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
