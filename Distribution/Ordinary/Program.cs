using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Ordinary
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region Load configuration

            Console.WriteLine(">> Finding root url ...");

            // Find root url which web api should be hosted at.
            var urlRoot = ConfigurationManager.AppSettings["RootUrl"];
            if (string.IsNullOrEmpty(urlRoot))
            {
                Console.WriteLine(
                    ">> No root url has been configured to host. Please specify it in: RootUrl of app.config");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($">> Root url is set to: {urlRoot}");

            #endregion

            #region Start hosting

            // Start hosting.
            WebApp.Start<Startup>(urlRoot);

            Console.WriteLine(">> Server running on {0}", urlRoot);
            Console.WriteLine(">> Press any key to stop hosting.");

            #endregion

            Console.ReadLine();
        }
    }
}