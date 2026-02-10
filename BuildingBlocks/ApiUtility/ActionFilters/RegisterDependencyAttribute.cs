using Microsoft.Extensions.DependencyInjection;

namespace ApiUtility.ActionFilters
{
    public class RegisterDependencyAttribute : Attribute
    {

        public ServiceLifetime Scope { get; }
        public RegisterDependencyAttribute(ServiceLifetime serviceLifetime)
        {
            Scope = serviceLifetime;
        }
    }

    public class RegisterSingletonAttribute : RegisterDependencyAttribute
    {
        public RegisterSingletonAttribute() : base(ServiceLifetime.Singleton)
        {
        }
    }
    public class RegisterScopedAttribute : RegisterDependencyAttribute
    {
        public RegisterScopedAttribute() : base(ServiceLifetime.Scoped)
        {
        }
    }
    public class RegisterTransientAttribute : RegisterDependencyAttribute
    {
        public RegisterTransientAttribute() : base(ServiceLifetime.Transient)
        {
        }
    }
}
//CustomerDbContext
//OrderDbContext
//StoreDbContext