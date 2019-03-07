using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Rolodex.Web
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<StartUp>();

        public static void Main(string[] args)
            => CreateWebHostBuilder(args).Build().Run();
    }
}