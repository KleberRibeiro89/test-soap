using Microsoft.Identity.Client;
using MiniHubApi.Host.Extensions;
using MiniHubApi.Infra.Ioc;
//using Sample.Observability.Infrastructure.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog(builder.Configuration, "API Observability");
    Log.Information("Starting API");
    //builder.Services.AddElasticsearch(builder.Configuration);
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddHttpContextAccessor();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddServices();
    builder.Services.AddInfra(builder.Configuration);
    builder.Services.AddElasticsearch(builder.Configuration);
    //builder.Services.AddMongoDb(builder.Configuration);
    //builder.Services.AddSqlServer(builder.Configuration);


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseMiddleware<LoggerMiddlewareExtensions>();
    app.UseAuthorization();

    app.MapControllers();
    app.UseSerilog();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}

