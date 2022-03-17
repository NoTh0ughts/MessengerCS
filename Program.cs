using Messenger.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Messenger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigLoader.Load(".env");
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
