using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace FRISS.Components.Utilities.Swagger
{
    public static class SwaggerMiddlewareExtension
    {
        public static void UseSwaggerMiddleware(this IApplicationBuilder app, string apiName, IConfiguration configuration)
        {

            var EnvironmentType = "";
            if (EnvironmentType.ToLower() != "prod")
            {
                string version = configuration.GetValue<string>("Swagger:SwaggerDocs:Version") ?? "v1";
                string swaggerEndpointUrl = $"/swagger/{version}/swagger.json";
                string routePrefix = configuration.GetValue<string>("Swagger:RoutePrefix") ?? "swagger";

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(swaggerEndpointUrl, apiName);
                    c.RoutePrefix = routePrefix;
                });
            }

        }

    }
}
