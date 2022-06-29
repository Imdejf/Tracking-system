using TrackingSystem.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace TrackingSystem.Shared.DependencyInjection
{
    public static partial class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<CurrentUserBindingMiddleware>();
            return app;
        }
    }
}
