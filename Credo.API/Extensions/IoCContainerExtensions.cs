using Credo.Domain.Interfaces.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Credo.Infrastructure.Services;

namespace Credo.API.Extensions
{
    public static class IoCContainerExtensions
    {
        public static void AddConventionalServices(this IServiceCollection services)
        {
            var transientType = typeof(ITransient);
            var scopedType = typeof(IScoped);
            var singletonType = typeof(ISingleton);

            var types = typeof(DapperService)
                .Assembly
                .GetExportedTypes()
                .Where(type => type.IsClass && !type.IsAbstract)
                .Select(type => new
                {
                    Service = type.GetInterface($"I{type.Name}"),
                    Implementation = type
                })
                .Where(t => t.Service != null).ToList();


            types.ForEach(type =>
            {
                if (transientType.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
                else if (scopedType.IsAssignableFrom(type.Service))
                {
                    services.AddSingleton(type.Service, type.Implementation);
                }
                else if (singletonType.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            });
        }
    }
}