using FRISS.Components.Utilities.APIKey;
using Microsoft.Extensions.DependencyInjection;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class APIKeyInjectionExtension
    {
        public static void InjectAPIKeyService(this IServiceCollection services)=>            
            services.AddSingleton<IAPIKeyValidator, APIKeyValidator>();
        
       
    }

}
