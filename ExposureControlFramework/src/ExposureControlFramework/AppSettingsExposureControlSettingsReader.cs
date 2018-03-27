namespace ExposureControlFramework
{
    using System.Configuration;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Implementation of the <see cref="IExposureControlSettingsReader"/> interface.
    /// </summary>
    public class AppSettingsExposureControlSettingsReader : IExposureControlSettingsReader
    {
        private const string ExposureControlSettingNamePrefix = "ECS";
               
        /// <summary>
        /// Retrieves an exposureControlSettings named <paramref name="name"/> from the repository.
        /// </summary>
        /// <param name="name">The requested exposureControlSettings name.</param>
        /// <returns>A <see cref="Task"/> returning the requested experient.</returns>
        public Task<T> GetExposureControlSettingsAsync<T>(string name) where T : ExposureControlSettings
        {
            Diagnostics.EnsureStringNotNullOrWhiteSpace(() => name);

            string configValue = ConfigurationManager.AppSettings[this.GetConfigurationPropertyName(name)];
            return Task.FromResult(configValue == null ? null : JsonConvert.DeserializeObject<T>(configValue));
        }
        
        private string GetConfigurationPropertyName(string exposureControlName)
        {
            return $"{ExposureControlSettingNamePrefix}_{exposureControlName}";
        }
    }
}
