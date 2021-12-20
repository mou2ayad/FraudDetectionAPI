using AutoMapper;
using Fraud.Api.Matching.Services;
using Fraud.Component.DataAccessLayer.DependencyInjection;
using Fraud.Component.Utilities.JWT_Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Fraud.Api.Matching.Bootstrappers
{
    public static class ServicesInjectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddDataAccessLayerProfiles();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

        public static IServiceCollection AddStaticServiceUser(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, StaticUserService>(); // this is just dev not for production
            return services;
        }
    }
}
