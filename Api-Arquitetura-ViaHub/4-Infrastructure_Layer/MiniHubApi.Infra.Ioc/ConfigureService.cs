using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniHubApi.Application.Interfaces;
using MiniHubApi.Application.Services;
using MiniHubApi.Application.Services.Logs;
using MiniHubApi.Infra.Soap;
using Nest;
using System.Data;
using System.Data.SqlClient;

namespace MiniHubApi.Infra.Ioc;
public static class ConfigureService
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDb(configuration)
            .AddSqlServer(configuration);
        //services.AddScoped<ICreditoRepository, MongoDb.Repository.CreditoRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<ILogCustomServices, LogCustomService>();
        services.AddScoped<ICreditoServices, CreditoServices>();
        services.AddHttpClient<ISoapService, SoapService>();
        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }

    public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var uri = configuration["ElasticsearchSettings:uri"];
        var defaultIndex = configuration["ElasticsearchSettings:defaultIndex"];
        var basicAuthUser = configuration["ElasticsearchSettings:username"];
        var basicAuthPassword = configuration["ElasticsearchSettings:password"];

        var settings = new ConnectionSettings(new Uri(uri));

        if (!string.IsNullOrEmpty(defaultIndex))
            settings = settings.DefaultIndex(defaultIndex);

        if (!string.IsNullOrEmpty(basicAuthUser) && !string.IsNullOrEmpty(basicAuthPassword))
            settings = settings.BasicAuthentication(basicAuthUser, basicAuthPassword);

        settings.EnableApiVersioningHeader();

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);
    }

    private static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration) 
    {
        return services;
    }

    private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddSingleton<IDbConnection>((sp) => new SqlConnection(configuration.GetSection("SqlServerSettings:ConnectionString").Value));
        return services;
    }
}