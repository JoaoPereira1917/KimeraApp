using Kimera.Api.Exceptions;
using Kimera.Application.Interfaces;
using Kimera.Infrastructure.Persistence.Configurations;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Diagnostics; // necessário para IExceptionHandler / IExceptionHandlerFeature
using Microsoft.AspNetCore.Http; // necessário para StatusCodes

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddInfra(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
// Registra explicitamente o KimeraExceptionHandler como implementação de
// Microsoft.AspNetCore.Diagnostics.IExceptionHandler

// extensão e garante que o handler esteja disponível.
builder.Services.AddSingleton<Microsoft.AspNetCore.Diagnostics.IExceptionHandler, KimeraExceptionHandler>();


var app = builder.Build();
// Configura o middleware de tratamento de exceções.

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = feature?.Error;

        // Resolve o handler a partir do provider do request
        var handler = context.RequestServices.GetService<Microsoft.AspNetCore.Diagnostics.IExceptionHandler>();

        if (handler is not null && exception is not null)
        {
            // Delegamos o tratamento para o KimeraExceptionHandler (TryHandleAsync)
            await handler.TryHandleAsync(context, exception, context.RequestAborted);
            return;
        }

        // Fallback simples caso o handler não esteja disponível
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("An unexpected error occurred.");
    });
});


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
