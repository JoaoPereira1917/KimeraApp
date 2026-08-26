using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Kimera.Api.Extensions
{
    /// <summary>
    /// Extensão de conveniência para registrar um IExceptionHandler no DI.
    /// Muitas bases de código adicionam um método AddExceptionHandler&lt;T&gt; para
    /// simplificar o registro; este arquivo implementa essa extensão caso ela
    /// não exista no projeto ou em algum pacote NuGet.
    /// </summary>
    public static class ExceptionHandlerServiceCollectionExtensions
    {
        /// <summary>
        /// Registra a implementação T como Microsoft.AspNetCore.Diagnostics.IExceptionHandler.
        /// Uso: builder.Services.AddExceptionHandler&lt;KimeraExceptionHandler&gt;();
        /// </summary>
        public static IServiceCollection AddExceptionHandler<T>(this IServiceCollection services)
            where T : class, IExceptionHandler
        {
            // Registra como singleton por ser stateless e reusável.
            services.AddSingleton<IExceptionHandler, T>();
            return services;
        }
    }
}
