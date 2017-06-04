using System.Web.Http;
using Ordinary.Configs;
using Owin;

namespace Ordinary
{
    public class Startup
    {

        #region Methods

        /// <summary>
        /// This code configures Web API. The Startup class is specified as a type parameter in the WebApp.Start method.
        /// </summary>
        /// <param name="appBuilder"></param>
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

            // Inversion of control registration.
            InversionOfControlConfig.Register(appBuilder, httpConfiguration);

            // Register web API module.
            appBuilder.UseWebApi(httpConfiguration);
        }

        #endregion
    }
}