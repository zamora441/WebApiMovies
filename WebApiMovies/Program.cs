using Serilog;
using Serilog.Events;
using WebApiMovies;
using WebApiMovies.Data.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", restrictedToMinimumLevel:LogEventLevel.Warning, rollingInterval: RollingInterval.Day, fileSizeLimitBytes : 10485760)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    var startup = new StartUp(builder.Configuration);
    startup.ConfigureServices(builder.Services);
    var app = builder.Build();
    startup.Configure(app, app.Environment);

    app.MigrateDatabase();
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}


