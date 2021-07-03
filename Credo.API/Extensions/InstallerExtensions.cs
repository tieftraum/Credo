using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Credo.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Credo.API.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var classesImplementingIInstaller = typeof(Startup).Assembly.ExportedTypes.Where(type =>
                typeof(IInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).ToList();

            classesImplementingIInstaller.ForEach(type =>
            {
                var instance = Activator.CreateInstance(type) as IInstaller;
                instance?.InstallService(services, configuration);
            });
        }
    }
}
