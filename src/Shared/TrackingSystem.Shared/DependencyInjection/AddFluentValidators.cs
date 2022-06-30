using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TrackingSystem.Shared.Exceptions;

namespace TrackingSystem.Shared.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services, params Assembly[] validatorsAssemblies)
        {
            services.AddMvc()
                .AddJsonOptions(c => {
                    c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    c.JsonSerializerOptions.MaxDepth = 32;
                    c.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .ConfigureApiBehaviorOptions(c => {
                    c.InvalidModelStateResponseFactory = c => {
                        throw new InvalidRequestException(c.ModelState.Keys.Select(a => a)
                            .ToDictionary(a => a, a => c.ModelState[a].Errors.Select(a => a.ErrorMessage).ToArray()));
                    };
                })
                .AddFluentValidation(c => {
                    c.RegisterValidatorsFromAssemblies(validatorsAssemblies);
                });

            return services;
        }
    }
}
