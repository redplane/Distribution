namespace Shared.Models.Messages
{
    public class MqServer
    {
        #region Properties

        public string Url { get; set; }

        /// <summary>
        /// Server of MQ service.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Username which is for accessing into queue service.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of queue account.
        /// </summary>
        public string Password { get; set; }

        #endregion
    }
}