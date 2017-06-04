using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Administration.Services;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json;
using Owin;
using Shared.Interfaces.Repositories;
using Shared.Models.Contexts;
using Shared.Models.Messages;
using Shared.Repositories;
using Shared.Services;

namespace Administration.Configs
{
    public class InversionOfControlConfig
    {
        #region Methods

        /// <summary>
        ///     Register list of inversion of controls.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="httpConfiguration"></param>
        public static void Register(IAppBuilder app, HttpConfiguration httpConfiguration)
        {
            // Initiate container builder to register dependency injection.
            var containerBuilder = new ContainerBuilder();

            #region Controllers & hubs

            // Controllers & hubs
            containerBuilder.RegisterApiControllers(typeof(Startup).Assembly);

            #endregion

            #region Unit of work & Database context

            // Database context initialization.
            containerBuilder.RegisterType<RelationalDbContext>().As<DbContext>().InstancePerLifetimeScope();

            // Unit of work registration.
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            // Register services.
            var systemFileService = new SystemFileService();
            containerBuilder.RegisterType<SystemFileService>().As<ISystemFileService>().OnActivating(x => x.ReplaceInstance(systemFileService)).InstancePerLifetimeScope();

            // Find queue setting.
            var mqOption =
                systemFileService.LoadJsonFile<MqServer>(ConfigurationManager.AppSettings["MqConfigurationFile"], false);
            containerBuilder.RegisterType<MqServer>().OnActivating(x => x.ReplaceInstance(mqOption)).SingleInstance();

            #endregion

            #region IoC build

            // Container build.
            var container = containerBuilder.Build();

            // Attach DI resolver.
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Attach dependency injection into configuration.
            app.UseAutofacWebApi(httpConfiguration);

            #endregion
        }
        
        #endregion
    }
}