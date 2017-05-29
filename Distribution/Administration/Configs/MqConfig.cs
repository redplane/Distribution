using System;
using System.Text;
using System.Web.Http;
using Administration.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Configuration;
using Shared.Services;
using Administration.Constants;

namespace Administration.Configs
{
    public class MqConfig
    {
        #region Properties

        /// <summary>
        /// Managing connection between client & queue service.
        /// </summary>
        private static ConnectionFactory _connectionFactory;

        /// <summary>
        /// Cloud settings.
        /// </summary>
        private static Dictionary<string, CloudBasicQueue> _cloudQueueConfig;

        /// <summary>
        /// Service which handles file operation.
        /// </summary>
        private static ISystemFileService _systemFileService;

        #endregion

        #region Methods
        

        /// <summary>
        /// Config queue with settings.
        /// </summary>
        public static void Configure(HttpConfiguration httpConfiguration)
        {
            // Find service from injection.
            _systemFileService = (ISystemFileService)httpConfiguration.DependencyResolver.GetService(typeof(ISystemFileService));

            // Load mq configuration from file.
            const string key = "MqConfigFile";
            var mqConfigFile = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(mqConfigFile))
                throw new Exception("Cannot find information bound to MqConfigFile in App.config");
            
            // Load option from file.
            var qOption = _systemFileService.LoadJsonFile<MqOption>(ConfigurationManager.AppSettings[key], false);
            if (qOption == null)
                throw new Exception("Cannot find information bound to MqConfigFile in App.config");

            // Initiate connection factory with specific information.
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.HostName = qOption.HostName;
            _connectionFactory.UserName = qOption.Username;
            _connectionFactory.VirtualHost = qOption.Username;
            _connectionFactory.Password = qOption.Password;
            
            // Load queues list.
            LoadQueuesList(httpConfiguration);

            // Initiate queue handlers.
            HandleAccountRegistrationEvent();
        }
        
        /// <summary>
        /// Handle account registration event.
        /// </summary>
        private static void HandleAccountRegistrationEvent()
        {
            // Find queue config.
            var qOption = _cloudQueueConfig[Queues.AccountRegistration];

            // Initiate connection to cloud message queue service.
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Queue declaration.
                channel.QueueDeclare(qOption.Name,
                                     qOption.Durable,
                                     qOption.IsExclusive,
                                     qOption.AutoDelete,
                                     arguments: null);

                // Initiate consumer and catch event.
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                
                // Consume message automatically.
                channel.BasicConsume(qOption.Name,
                                     qOption.AutoAcknowledge,
                                     consumer);
            }
        }
        
        /// <summary>
        /// Load queues list from configuration file.
        /// </summary>
        /// <param name="httpConfiguration"></param>
        private static void LoadQueuesList(HttpConfiguration httpConfiguration)
        {
            // Find queues list in configuration file.
            const string key = "MqListConfigurationFile";

            // Find configuration file in setting key.
            var mqConfigurationFile = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(mqConfigurationFile))
                throw new Exception($"Queues configuration file doesn't exist. Please specify configuration file using {key} in App.config");

            // Find file service in HttpConfiguration.
            _cloudQueueConfig = _systemFileService.LoadJsonFile<Dictionary<string, CloudBasicQueue>>(mqConfigurationFile, false);
        }

        #endregion
    }
}