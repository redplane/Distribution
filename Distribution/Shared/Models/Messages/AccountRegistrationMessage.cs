namespace Shared.Models.Messages
{
    public class AccountRegistrationMessage
    {
        #region Properties

        /// <summary>
        /// Email which is registered into system.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Time when the registration was done.
        /// </summary>
        public double Time { get; set; }

        #endregion
    }
}