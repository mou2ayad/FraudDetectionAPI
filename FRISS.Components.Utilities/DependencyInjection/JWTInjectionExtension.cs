using FRISS.Components.Utilities.JWT_Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class JWTInjectionExtension
    {
        public static void InjectJWTService(this IServiceCollection services,IConfiguration configuration) =>    
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
    }

}
