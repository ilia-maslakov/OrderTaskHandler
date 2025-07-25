using System;
using Camunda.Worker;
using Camunda.Worker.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderTaskHandler;
using OrderTaskHandler.Handlers;
using OrderTaskHandler.Services;

namespace OrderTaskHandler.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegistrateApplicationServices(this IServiceCollection services)
        {
            services.AddExternalTaskClient(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8050/engine-rest");
            });

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IReportService, ReportService>();

            services.AddCamundaWorker("sampleWorker")
                .AddHandler<CreateOrderTaskHandler>()
                .AddHandler<GenerateReportHandler>()
                .AddHandler<SendReportHandler>()
                .ConfigurePipeline(pipeline =>
                {
                    pipeline.Use(next => async context =>
                    {
                        var logger = context.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                        logger.LogInformation("Started processing of task {Id} by worker {WorkerId}", context.Task.Id, context.Task.WorkerId);
                        await next(context);
                        logger.LogInformation("Finished processing of task {Id}", context.Task.Id);
                    });
                });

            services.AddHttpClient<ICamundaClient, CamundaClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8050");
            });

            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddTransient<IOrderService, OrderService>();

            return services;
        }
    }
}