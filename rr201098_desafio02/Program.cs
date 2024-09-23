using AuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOcelot();

var app = builder.Build();

app.UseMiddleware<ErrorManager>();
app.UseMiddleware<LoginController>();
app.UseMiddleware<TokenValidator>();

app.UseOcelot().Wait();

app.Run();
