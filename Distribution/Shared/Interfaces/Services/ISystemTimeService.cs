using System;

namespace Shared.Interfaces.Services
{
    public interface ISystemTimeService
    {
        #region Methods

        /// <summary>
        /// Convert UTC to Epoch.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        double UtcToEpoch(DateTime dateTime);

        /// <summary>
        /// Convert epoch time to Utc
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        DateTime EpochToUtc(double unixTime);

        #endregion
    }
}