using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Domain;
using NotificationService.Infrastructure.DB;
using NotificationService.Infrastructure.NotificationServices;

namespace NotificationService.Infrastructure;

public static class Registration
{
    public static void RegisterInfrastructureDependency(this IServiceCollection services)
    {
        services.AddSingleton<EmailService, EmailService>();
        services.AddSingleton<SMSService, SMSService>();
        services.AddSingleton<PushNotificationService, PushNotificationService>();

        services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase(databaseName: "TestDatabase"));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
