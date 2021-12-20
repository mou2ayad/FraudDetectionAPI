using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using NLog;
using System;

namespace Fraud.Api.Matching
{
    public class Program
    {
        static  Logger logger;

        public static void Main(string[] args)
        {
            logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("init new instance from Cryptocyrrency API");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped FraudMatching API because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
