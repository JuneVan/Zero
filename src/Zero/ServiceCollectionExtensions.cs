namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZero(this IServiceCollection services)
        {
            services.AddScoped<IIdentifier, NullIdentifier>();
            services.AddScoped<IThreadSignal, NullThreadSignal>();
            services.AddScoped<IEntityChangeEventHelper, EntityChangeEventHelper>();
            services.AddScoped<IPermissionChecker, PermissionChecker>();
            services.AddScoped<IPermissionStore, NullPermissionStore>();
            return services;
        }
    }
}
