using ApiUtility.ActionFilters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services, string assemblyName)
        {
            var types = Assembly.Load(assemblyName)
                .GetTypes();

            var interfaces = types.Where(t => t.GetCustomAttribute<RegisterDependencyAttribute>() != null && t.IsInterface);

            foreach (var @interface in interfaces)
            {
                var @class = GetImplementedClass(types, @interface);
                AddServicedescriptor(services, @interface, @class);
            }
        }
        private static void AddServicedescriptor(IServiceCollection services, Type @type, Type @class)
        {
            var scope = @type.GetCustomAttribute<RegisterDependencyAttribute>().Scope;
            services.Add(new ServiceDescriptor(@type, @class, scope));
        }

        public static Type GetImplementedClass(Type[] types, Type @interface)
        {
            var @class = types.FirstOrDefault(t => t.GetTypeInfo().ImplementedInterfaces.Any(i => i == @interface));

            return @class ?? throw new NotImplementedException($"No class implements Interface: {@interface.Name}");
        }
    }
}
