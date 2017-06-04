using System.Collections.Generic;
using Shared.Models.Messages;

namespace Ordinary.Interfaces
{
    public interface IMqService
    {
        #region Methods

        /// <summary>
        /// Import server setting.
        /// </summary>
        /// <param name="mqServer"></param>
        void SetServer(MqServer mqServer);

        /// <summary>
        /// Set mq list.
        /// </summary>
        /// <param name="mqList"></param>
        void SetMq(Dictionary<string, MqBasic> mqList);

        /// <summary>
        /// Find queue from registered list.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        MqBasic FindQueue(string name);

        /// <summary>
        /// Broadcast a message to a specific queue.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        void Send(string queueName, object message);

        #endregion
    }
}