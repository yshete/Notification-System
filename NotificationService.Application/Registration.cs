using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Handlers;
using NotificationService.Application.Handlers.Implementation;

namespace NotificationService.Application;

public static class Registration
{
    public static void RegisterApplicationDependency(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler,NotificationHandler>();
        services.AddScoped<INotificationTypeHandler,NotificationTypeHandler>();
    }
}
