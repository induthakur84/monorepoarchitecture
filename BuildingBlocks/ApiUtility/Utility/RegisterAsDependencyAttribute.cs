using Microsoft.Extensions.DependencyInjection;

namespace SharedModel.Utility
{
    public class RegisterAsDependencyAttribute : Attribute
    {
        protected RegisterAsDependencyAttribute(ServiceLifetime requiredLifetime)
        {
            Scope = requiredLifetime;
        }

        public ServiceLifetime Scope { get; }
    }

    public class RegisterScopedAttribute : RegisterAsDependencyAttribute
    {
        public RegisterScopedAttribute() : base(ServiceLifetime.Scoped)
        { }
    }

    public class RegisterTransientAttribute : RegisterAsDependencyAttribute
    {
        public RegisterTransientAttribute() : base(ServiceLifetime.Transient)
        { }
    }

    public class RegisterSingletonAttribute : RegisterAsDependencyAttribute
    {
        public RegisterSingletonAttribute() : base(ServiceLifetime.Singleton)
        { }
    }
}
