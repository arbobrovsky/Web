using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //BuildWebHost(args).Run();
        }
        //public static IWebHost BuildWebHost(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //       .UseStartup<Startup>()
        //       .UseKestrel(options =>
        //       {
        //           options.Limits.MaxConcurrentConnections = 100;
        //           options.Limits.MaxRequestBodySize = 10 * 1024;
        //           options.Limits.MinRequestBodyDataRate =
        //               new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
        //           options.Limits.MinResponseDataRate =
        //               new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
        //           options.Listen(IPAddress.Loopback, 5000);
        //       })
        //       .Build();


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider(options => options.ValidateScopes = false) // << add this line
                .UseStartup<Startup>();
    }
}
