using System;
using System.Configuration;
using Administration.Services;
using Microsoft.Owin.Hosting;
using Shared.Services;

namespace Administration
{
    internal class Program
    {
        #region Methods

        /// <summary>
        ///     Runs when application starts.
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            #region Configuration lookup

            // Find root url of API.
            var rootUrl = ConfigurationManager.AppSettings["RootUrl"];
            if (string.IsNullOrEmpty(rootUrl))
            {
                Console.WriteLine("Could not find root url of configuration");
                Console.ReadLine();
                return;
            }

            #endregion

            #region Start host

            Console.WriteLine(">> Startin service at: {0}", rootUrl);
            WebApp.Start<Startup>(rootUrl);
            
            Console.WriteLine(">> Host started at: {0}", rootUrl);
            Console.WriteLine(">> Press any key to stop");
            Console.ReadLine();

            #endregion
        }

        #endregion
    }
}