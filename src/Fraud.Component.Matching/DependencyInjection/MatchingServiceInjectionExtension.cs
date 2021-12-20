using Fraud.Component.Common.Models;
using Fraud.Component.Matching.Configuration;
using Fraud.Component.Matching.Contracts;
using Fraud.Component.Matching.Models;
using Fraud.Component.Matching.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fraud.Component.Matching.DependencyInjection
{
    public static class MatchingServiceInjectionExtension
    {
        public static IServiceCollection AddMatchingService(this IServiceCollection container, IConfiguration configuration)
        {
            container.Configure<MatchingConfig>(configuration.GetSection("MatchingConfig"));
            container.Configure<TypoDetectorConfig>(configuration.GetSection("TypoDetectorConfig"));
            container.AddSingleton<INameSimilarity,InitialsMatchingService>();
            container.AddSingleton<INameSimilarity, TypoDetectorService>();
            container.AddSingleton<INameSimilarity, NickNameDetectorService>();
            container.AddTransient(typeof(IMatchingService<Person>), typeof(MatchingService<Person>));
            if (configuration.GetValue<bool>("MatchingConfig:EnableCache"))
            {
                container.Decorate(typeof(IMatchingService<Person>), typeof(CacheMatchingServiceDecorator<Person>));
            }
            return container;
        }

        public static void UseMatchingRules(this IApplicationBuilder app, IOptions<MatchingConfig> options) => 
            MatchingRules.SetRange(options.Value.MatchingRules.ToArray());
    }
}
