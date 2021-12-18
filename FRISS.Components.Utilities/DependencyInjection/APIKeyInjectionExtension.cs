using FRISS.Components.Utilities.APIKey;
using Microsoft.Extensions.DependencyInjection;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class ApiKeyInjectionExtension
    {
        public static void InjectApiKeyService(this IServiceCollection services)=>            
            services.AddSingleton<IAPIKeyValidator, APIKeyValidator>();
        
    }

}
