namespace Zero.Configuration.Nacos
{
    public static class IHostBuilderExtensions
    {
        /// <summary>
        /// 添加Nacos配置
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddNacosConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(delegate (HostBuilderContext context, IConfigurationBuilder builder)
            {
                builder.AddNacosV2Configuration(delegate (NacosV2ConfigurationSource configure)
                {
                    var configuration = builder.Build();
                    configure.ServerAddresses = new List<string> { Environment.GetEnvironmentVariable("NACOSBASE_SERVERADDRESS")!.ToString() };
                    configure.DefaultTimeOut = Convert.ToInt32(Environment.GetEnvironmentVariable("NACOSBASE_DEFAULTTIMEOUT")!.ToString());
                    configure.Namespace = Environment.GetEnvironmentVariable("NACOSBASE_NAMESPACE")!.ToString();
                    configure.UserName = Environment.GetEnvironmentVariable("NACOSBASE_USERNAME")!.ToString();
                    configure.Password = Environment.GetEnvironmentVariable("NACOSBASE_PASSWORD")!.ToString();

                    configure.ConfigUseRpc = false;
                    configure.NamingUseRpc = false;
                    var listeners = configuration.GetSection("Nacos:Listeners")?.Get<List<ConfigListener>>();
                    if (listeners != null && listeners.Any())
                        configure.Listeners = listeners;

                });
                // 重新读取配置（保证环境配置优先）  
                if (!string.IsNullOrEmpty(context.HostingEnvironment.EnvironmentName))
                {
                    builder = builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true);
                }
            });

            return hostBuilder;

        }
    }
}