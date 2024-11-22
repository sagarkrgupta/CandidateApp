using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business
{
    public static class RegisterServicesExtension
    {
        public static void RegisterScopedServices(this IServiceCollection services)
        {            
            services.AddServicesFromAssembly(Assembly.GetExecutingAssembly());

        }

        public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("AppServices") && !t.IsInterface)
                .ToList();

            foreach (var type in serviceTypes)
            {
                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    services.AddScoped(@interface, type);
                }
            }
        }

    }
}
