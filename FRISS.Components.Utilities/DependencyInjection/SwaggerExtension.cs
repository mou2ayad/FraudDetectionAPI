using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FRISS.Components.Utilities.DependencyInjection
{
    public static class SwaggerExtension
    {
        public static void InjectSwaggerServices(this IServiceCollection services, string APITitle, IConfiguration configuration)
        {

            var EnvironmentType = "";
            if (EnvironmentType.ToLower() != "prod")
            {
                OpenApiInfo swaggerDoc = new OpenApiInfo()
                {
                    Title = APITitle,
                    Version = configuration.GetValue<string>("Swagger:Version") ?? "v1",
                    Description = configuration.GetValue<string>("Swagger:Description") ?? APITitle + " Description"
                };
                if (configuration.GetSection("Swagger:Contact").Exists())
                {
                    Uri uri = null;
                    Uri.TryCreate(configuration.GetValue<string>("Swagger:Contact:Url"), UriKind.Absolute, out uri);
                    swaggerDoc.Contact = new OpenApiContact()
                    {
                        Name = configuration.GetValue<string>("Swagger:Contact:Name") ?? "Monster",
                        Email = configuration.GetValue<string>("Swagger:Contact:Email") ?? "Monster@monster.com",
                        Url = uri
                    };
                }
                var jwtAuth = configuration.GetValue<bool>("Swagger:JWTAuthentication");
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(swaggerDoc.Version, swaggerDoc);
                    if (jwtAuth)
                    {
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",

                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                        {
                           
                            {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }

                    });
                    }


                });

            }

        }

    }
}
