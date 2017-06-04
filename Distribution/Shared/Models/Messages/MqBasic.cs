namespace Shared.Models.Messages
{
    public class MqBasic
    {
        #region Properties

        /// <summary>
        /// Name of queue which is used for exchanging messages.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// Kind of queue which is used for broadcasting information.
        /// </summary>
        public string KindOfQueue { get; set; }

        /// <summary>
        /// If set when creating a new exchange, the exchange will be marked as durable. Durable exchanges remain active when a server restarts. Non-durable exchanges (transient exchanges) are purged if/when a server restarts.
        /// The server MUST support both durable and transient exchanges.
        /// </summary>
        public bool Durable { get; set; }

        /// <summary>
        /// If set, the exchange is deleted when all queues have finished using it.
        /// The server SHOULD allow for a reasonable delay between the point when it determines that an exchange is not being used(or no longer used), and the point when it deletes the exchange.At the least it must allow a client to create an exchange and then bind a queue to it, with a small but non-zero delay between these two actions.
        /// The server MUST ignore the auto-delete field if the exchange already exists.
        /// </summary>
        public bool AutoDelete { get; set; }

        /// <summary>
        /// Whether acknowlege message should be sent to queue and a message has been consumed or not.
        /// </summary>
        public bool AutoAcknowledge { get; set; }

        /// <summary>
        /// Name of queue.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If the no-local field is set the server will not send messages to the connection that published them.
        /// </summary>
        public bool IsNoLocal { get; set; }

        /// <summary>
        /// Exclusive queues may only be accessed by the current connection, and are deleted when that connection closes. Passive declaration of an exclusive queue by other connections are not allowed.
        /// </summary>
        public bool IsExclusive { get; set; }

        /// <summary>
        /// Specifies the identifier for the consumer.
        /// The consumer tag is local to a channel, so two clients can use the same consumer tags. If this field is empty the server will generate a unique tag.
        /// </summary>
        public string ConsumerTag { get; set; }

        #endregion
    }
}
