using System;
using System.Text;
using System.Web.Http;
using Administration.Configs;
using Administration.Models;
using Owin;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Administration
{
    public class Startup
    {
        #region Methods

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var httpConfiguration = new HttpConfiguration();

            // Use routing attribute.
            httpConfiguration.MapHttpAttributeRoutes();

            // Route navigation configuration.
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            // Register inversion of control.
            InversionOfControlConfig.Register(appBuilder, httpConfiguration);

            // Config queue.
            MqConfig.Configure(httpConfiguration);

            // Register web API module.
            appBuilder.UseWebApi(httpConfiguration);

        }
        
        #endregion
    }
}