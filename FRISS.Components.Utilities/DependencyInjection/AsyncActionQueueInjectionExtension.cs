using FRISS.Components.Utilities.Async;
using Microsoft.Extensions.DependencyInjection;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class AsyncActionQueueInjectionExtension
    {
        public static void InjectAsyncActionQueueServices(this IServiceCollection services) =>
            services.AddSingleton<IAsyncActionQueue, AsyncActionQueue>();           
        
    }
}
