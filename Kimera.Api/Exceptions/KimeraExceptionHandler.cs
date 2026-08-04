using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Kimera.Api.Exceptions
{
    public class KimeraExceptionHandler : IExceptionHandler
    {
        private readonly ILogger _logger;
        public KimeraExceptionHandler(ILogger<KimeraExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //Mapear exceção para status HTTP + título
            var (status, title) = exception switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
                InvalidOperationException => (StatusCodes.Status409Conflict, " Conflict operation"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument"),
                _ => (StatusCodes.Status500InternalServerError, "Internal server error")
            };
            // log estruturado - nível varia conforme gravidade
            if (status == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            }
            else
            {
                _logger.LogWarning(exception, "Handled exception occurred: {Message}", exception.Message);

            }
            //monta a resposta ProblemDetatils
            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };
            // Define o status code da resposta
            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = status;
            // Retorna o problema como JSON
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            //treated exception
            return true;
        }
    }
}
