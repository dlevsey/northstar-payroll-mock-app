using System.Text.Json.Serialization;
using Northstar.Application;
using Northstar.Infrastructure;
using Northstar.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Northstar.Api.Security.HeaderUserContext>();
builder.Services.AddCors(options => options.AddPolicy("DevelopmentSpa", policy =>
    policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("DevelopmentSpa");
}

app.UseExceptionHandler();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));
app.MapControllers();

if (app.Configuration.GetValue("SeedData", true))
{
    await using var scope = app.Services.CreateAsyncScope();
    await DatabaseInitializer.InitializeAsync(
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
}

app.Run();

public partial class Program;
