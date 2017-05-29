namespace Administration.Models
{
    public class MqOption
    {
        #region Properties

        /// <summary>
        /// HostName of MQ service.
        /// </summary>
        public string HostName { get; set; }

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