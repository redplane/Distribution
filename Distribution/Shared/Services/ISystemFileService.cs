namespace Shared.Services
{
    public interface ISystemFileService
    {
        #region Methods

        /// <summary>
        /// Load configuration from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isAbsolute"></param>
        /// <returns></returns>
        T LoadJsonFile<T>(string path, bool isAbsolute);

        #endregion
    }
}