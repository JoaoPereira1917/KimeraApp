using Kimera.Api.Exceptions;
using Kimera.Application.Interfaces;
using Kimera.Infrastructure.Persistence.Configurations;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<KimeraExceptionHandler>();


var app = builder.Build();
app.UseExceptionHandler(options => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection();
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Kimera API v1");
        options.RoutePrefix = "swagger"; // Acessível em /swagger
    });
}



app.UseAuthorization();

app.MapControllers();

app.Run();
