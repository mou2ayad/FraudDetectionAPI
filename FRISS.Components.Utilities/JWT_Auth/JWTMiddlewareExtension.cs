using Microsoft.AspNetCore.Builder;

namespace FRISS.Components.Utilities.JWT_Auth
{
    public static class JwtMiddlewareExtension
    {
        public static void UseJwtMiddleware(this IApplicationBuilder app) =>
          app.UseMiddleware<JwtMiddleware>();
    }
}
