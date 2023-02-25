using AuthorizationOptions = Zero.AspNetCore.Authorization.AuthorizationOptions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZeroAspNetCore(this IServiceCollection services, Action<AuthorizationOptions> configure)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddScoped<IIdentifier, ClaimIdentifier>();
            services.Replace(new ServiceDescriptor(typeof(IThreadSignal), typeof(HttpThreadSignal), ServiceLifetime.Scoped));
            var options = new AuthorizationOptions();
            configure?.Invoke(options);
            services.Configure(configure);
            services.Replace(new ServiceDescriptor(typeof(IPermissionStore), typeof(CachePermissionStore), ServiceLifetime.Scoped));
#pragma warning disable CS0618
            services.AddTransient<IValidatorFactory, FluentValidationValidatorFactory>();
#pragma warning restore CS0618 

            services.Configure<MvcOptions>(configure =>
            {
                // 需关闭自动验证
                configure.Filters.Add<FluentValidationValidationFilter>();
                configure.Filters.Add<ResultWrapperFilter>();
                configure.Filters.Add<PermissionFilter>();
                configure.Filters.Add<ExceptionFilter>();
            });
            return services;
        }
    }
}
