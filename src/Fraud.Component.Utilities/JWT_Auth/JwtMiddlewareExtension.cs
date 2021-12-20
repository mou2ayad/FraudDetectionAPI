using Microsoft.AspNetCore.Builder;

namespace Fraud.Component.Utilities.JWT_Auth
{
    public static class JwtMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtMiddleware>();
            return app;
        }
    }
}
