﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ordinary.Interfaces;
using RabbitMQ.Client;
using Shared.Models.Messages;

namespace Ordinary.Services
{
    public class MqService : IMqService
    {
        #region Properties

        /// <summary>
        /// List of basic queues.
        /// </summary>
        private IDictionary<string, MqBasic> _mqBasics;

        /// <summary>
        /// Queue server setting.
        /// </summary>
        private ConnectionFactory _connectionFactory;

        #endregion

        #region Methods

        /// <summary>
        /// Set queue server information.
        /// </summary>
        /// <param name="mqServer"></param>
        public void SetServer(MqServer mqServer)
        {
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.Uri = mqServer.Url;
            _connectionFactory.HostName = mqServer.Server;
            _connectionFactory.UserName = mqServer.Username;
            _connectionFactory.Password = mqServer.Password;
        }

        /// <summary>
        /// Set mq configuration.
        /// </summary>
        /// <param name="mqList"></param>
        public void SetMq(Dictionary<string, MqBasic> mqList)
        {
            _mqBasics = mqList;
        }

        /// <summary>
        /// Find queue configuration from registered list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MqBasic FindQueue(string name)
        {
            if (_mqBasics == null)
                return null;

            if (_mqBasics.ContainsKey(name))
                return null;

            return _mqBasics[name];
        }

        /// <summary>
        /// Send message to a specific queue.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public void Send(string queueName, object message)
        {
            // Serialize message to string.
            var szMessage = JsonConvert.SerializeObject(message);

            throw new NotImplementedException();
        }

        #endregion
    }
}