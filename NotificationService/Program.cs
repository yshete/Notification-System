using NotificationService.Application;
using NotificationService.Filters;
using NotificationService.Infrastructure;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<CustomExceptionFilter>(); // Register globally
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.RegisterApplicationDependency();
        builder.Services.RegisterInfrastructureDependency();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        app.Run();
    }
}