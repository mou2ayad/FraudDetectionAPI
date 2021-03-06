using System;
using Fraud.Api.Matching.Bootstrappers;
using Fraud.Component.DataAccessLayer.DependencyInjection;
using Fraud.Component.Matching.Configuration;
using Fraud.Component.Matching.DependencyInjection;
using Fraud.Component.Utilities.DependencyInjection;
using Fraud.Component.Utilities.JWT_Auth;
using Fraud.Component.Utilities.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Fraud.Component.Utilities.ErrorHandling;

namespace Fraud.Api.Matching
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string API_NAME = "FraudMatchingApi";

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHealthChecks();
            services.AddMemoryCache()
                .AddAutoMapper()
                .AddDataAccessLayerServices(Configuration)
                .AddMatchingService(Configuration)
                .AddCacheService(Configuration)
                .AddStaticServiceUser() // this is just dev not for production
                .AddSwaggerService(API_NAME, Configuration)
                .AddJwtService(Configuration);

        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> log, IWebHostEnvironment env, IServiceProvider container)
        {
            app.UseSwaggerMiddleware(API_NAME, Configuration).
                UseFraudErrorHandler(log, API_NAME, env.IsDevelopment())
                .UseHttpsRedirection()
                .UseRouting()
                .UseJwtMiddleware()
                .UseHealthChecks(new Microsoft.AspNetCore.Http.PathString("/healthcheck"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMatchingRules(container.GetService<IOptions<MatchingConfig>>());
        }
    }
}
