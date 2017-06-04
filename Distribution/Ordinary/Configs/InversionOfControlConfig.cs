using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Ordinary.Interfaces;
using Ordinary.Services;
using Owin;
using Shared.Interfaces.Repositories;
using Shared.Models.Contexts;
using Shared.Models.Messages;
using Shared.Repositories;
using Shared.Services;

namespace Ordinary.Configs
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
            
            // Initiate system file service.
            var systemFileService = new SystemFileService();;
            containerBuilder.RegisterType<SystemFileService>().As<ISystemFileService>().OnActivating(x => x.ReplaceInstance(systemFileService)).InstancePerLifetimeScope();

            // Initiate mq service.
            var mqService = LoadMq(systemFileService);
            containerBuilder.RegisterType<MqService>()
                .As<IMqService>()
                .OnActivating(x => x.ReplaceInstance(mqService))
                .InstancePerLifetimeScope();

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

        /// <summary>
        /// Load MQ Service configuration.
        /// </summary>
        /// <returns></returns>
        private static IMqService LoadMq(ISystemFileService systemFileService)
        {
            var mqService = new MqService();

            // Load mq server.
            const string mqServerConfigurationKey = "MqServerConfigurationFile";
            var mqServerConfigurationFile = ConfigurationManager.AppSettings[mqServerConfigurationKey];
            if (string.IsNullOrEmpty(mqServerConfigurationFile))
                throw new Exception($"MQ Server configuration file is not found. Please check {mqServerConfigurationKey} in App.config");

            // Load server information.
            var mqServer = systemFileService.LoadJsonFile<MqServer>(mqServerConfigurationFile, false);
            mqService.SetServer(mqServer);

            // Load queues list.
            const string mqListConfigurationKey = "MqListConfigurationFile";
            var mqListConfigurationFile = ConfigurationManager.AppSettings[mqListConfigurationKey];
            if (string.IsNullOrEmpty(mqListConfigurationFile))
                throw new Exception($"MQ Server configuration file is not found. Please check {mqListConfigurationKey} in App.config");

            var mqBasics = systemFileService.LoadJsonFile<Dictionary<string, MqBasic>>(mqListConfigurationFile, false);
            mqService.SetMq(mqBasics);

            return mqService;
        }
        #endregion
    }
}