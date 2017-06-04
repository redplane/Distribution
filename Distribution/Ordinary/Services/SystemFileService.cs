using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Shared.Services;

namespace Ordinary.Services
{
    public class SystemFileService : ISystemFileService
    {
        #region Methods

        /// <summary>
        /// Load json object from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isAbsolute"></param>
        /// <returns></returns>
        public T LoadJsonFile<T>(string path, bool isAbsolute)
        {
            // Path is incorrect.
            if (string.IsNullOrEmpty(path))
                return default(T);

            // Find executing assembly.
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(applicationPath))
                return default(T);

            // Build absolute path.
            if (isAbsolute)
                applicationPath = path;
            else
                applicationPath = Path.Combine(applicationPath, path);

            // File doesn't exist.
            if (!File.Exists(applicationPath))
                return default(T);

            // Read all content in application path.
            var content = File.ReadAllText(applicationPath);

            // Deserialize and return object.
            return JsonConvert.DeserializeObject<T>(content);
        }

        #endregion
    }
}