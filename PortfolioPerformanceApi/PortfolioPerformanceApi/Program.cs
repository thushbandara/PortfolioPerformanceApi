
using PortfolioPerformance.Api.Infrastructure;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddCors();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // Discover and register all IEndpoint implementations dynamically
    var endpointTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => typeof(IEndpoint).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

    foreach (var endpointType in endpointTypes)
    {
        if (Activator.CreateInstance(endpointType) is IEndpoint endpointInstance)
        {
            endpointInstance.Configure(endpoints);
        }
    }
});

app.Run();