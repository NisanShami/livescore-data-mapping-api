using System.Threading.Tasks;
using LSports.Core.Hosting.Interfaces;
using LSports.Extensions.Logging;
using LSports.Hosting.Http.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.Placeholder;

namespace LSports.DataMapping.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await LSportsWebHost.CreateDefaultWebHost<Startup>()
            .AddPlaceholderResolver()
            .ConfigureAppConfiguration((x, y) =>
                {
                    LSportsHostingContext.Configuration = y.Build();
                })
            .ConfigureLogging((_, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddLSportsSerilogProviderFromConfiguration();
                })
            .Build()
            .RunAsync();
        }
    }
}
