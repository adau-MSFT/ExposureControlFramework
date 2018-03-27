namespace ExposureControlFramework
{
    using System.Threading.Tasks;

    public interface IExposureControlSettingsReader
    {
        /// <summary>
        /// Retrieves an exposureControlSettings named <paramref name="name"/> from the repository.
        /// </summary>
        /// <param name="name">The requested exposureControlSettings name.</param>
        /// <returns>A <see cref="Task"/> returning the requested experient.</returns>
        Task<T> GetExposureControlSettingsAsync<T>(string name) where T : ExposureControlSettings;
    }
}
