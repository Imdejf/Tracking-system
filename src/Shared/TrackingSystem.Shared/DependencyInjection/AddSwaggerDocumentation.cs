using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;


namespace TrackingSystem.Shared.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            string callingAssemblyName = Assembly.GetCallingAssembly().GetName().Name;
            services.AddSwaggerGen(c => {
                c.SwaggerDoc(callingAssemblyName, new OpenApiInfo
                {
                    Title = callingAssemblyName,
                    Description = "",
                    Version = "v1"
                });

                if (System.IO.File.Exists($"{callingAssemblyName}.xml"))
                    c.IncludeXmlComments($"{callingAssemblyName}.xml", true);

                c.SwaggerGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGeneratorOptions
                {
                    IgnoreObsoleteActions = true
                };

                c.SchemaGeneratorOptions = new Swashbuckle.AspNetCore.SwaggerGen.SchemaGeneratorOptions
                {
                    IgnoreObsoleteProperties = true
                };

                c.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert JWT  \n Example :  bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                              Type = ReferenceType.SecurityScheme,
                              Id = "JwtBearer"
                            }
                        },
                        System.Array.Empty<string>()
                    }
                });

                c.CustomSchemaIds(c => c.FullName);
            });
            return services;
        }
    }

    public static partial class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerOpenAPI(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var callingAssemblyName = Assembly.GetCallingAssembly().GetName().Name;
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", $"{callingAssemblyName} v1"));
            }
            return app;
        }
    }
}