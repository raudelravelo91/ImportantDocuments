using Microsoft.EntityFrameworkCore;
using ImportantDocuments.API;
using ImportantDocuments.API.Middleware;
using ImportantDocuments.API.Services;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    //.AddJsonOptions(options =>
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IDocService, DocService>();
    builder.Services.AddScoped<IAppDbContext, AppDbContext>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.UseAuthorization();

    // Global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    
    app.UseMiddleware<LoggingMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    // NLog: catch setup errors
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}