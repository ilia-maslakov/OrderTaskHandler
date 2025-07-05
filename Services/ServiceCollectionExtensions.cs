using Microsoft.Extensions.DependencyInjection;

namespace SampleCamundaWorker.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddHttpClient<ICamundaClient, CamundaClient>();
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}
