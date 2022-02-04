using Orleans.Hosting;
using Orleans;
using Orleans.Configuration;

namespace OrleansTesting
{
    public class Program
    {
        public static IConfiguration Config { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseOrleans(Startup.ConfigureOrleans(Config))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}