using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => { options.DescribeAllParametersInCamelCase(); });
services.AddRouting(options => options.LowercaseUrls = true);

var application = builder.Build();

application.UseSerilogRequestLogging();

application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

application.UseRouting();
application.MapControllers();

application.Run();

public partial class Program
{
} // Public modifier is needed for WebApplicationFactory<Program> instance creation in integration tests
