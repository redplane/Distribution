using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;

namespace NotificationCenter
{
    public class Startup
    {
        #region Methods

        /// <summary>
        ///     Method which is call when component has been configured.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // Web API configuration.
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            // Turn on cross origin resource sharing.
            app.UseCors(CorsOptions.AllowAll);

            // Use signalr.
            app.MapSignalR();

            // Turn on Web API configuration.
            app.UseWebApi(httpConfiguration);
        }

        #endregion
    }
}