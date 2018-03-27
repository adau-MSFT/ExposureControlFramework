namespace ExposureControlFramework
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExposureControlSettingsManager
    {
        /// <summary>
        /// Creates a new exposureControlSettings in the repository.
        /// </summary>
        /// <param name="exposureControlSettings">The exposureControlSettings to create.</param>
        /// <returns>A <see cref="Task"/> running the async operation.</returns>
        /// <exception cref="ExposureControlSettingsAlreadyExistsException">If an exposureControlSettings with the same name already exists in the repository.</exception>
        Task CreateExposureControlSettingsAsync<T>(T exposureControlSettings) where T : ExposureControlSettings;

        /// <summary>
        /// Updates an existing exposureControlSettings in the repository.
        /// </summary>
        /// <param name="exposureControlSettings">The exposureControlSettings to update.</param>
        /// <returns>A <see cref="Task"/> running the async operation.</returns>
        /// <exception cref="ExposureControlSettingsNotFoundException">If the exposureControlSettings is not found in the repository.</exception>
        Task UpdateExposureControlSettingsAsync<T>(T exposureControlSettings) where T : ExposureControlSettings;

        /// <summary>
        /// Deletes an exposureControlSettings from the repsitory.
        /// </summary>
        /// <param name="name">The name of the exposureControlSettings to delete.</param>
        /// <returns>A <see cref="Task"/> returning the deleted exposureControlSettings.</returns>
        /// <exception cref="ExposureControlSettingsNotFoundException">If the exposureControlSettings is not found in the repository.</exception>
        Task DeleteExposureControlSettingsAsync<T>(string name) where T : ExposureControlSettings;


        /// <summary>
        /// Retrieves all experiments from the repsitory.
        /// </summary>
        /// <returns>A <see cref="Task"/> returning the requested experiments.</returns>
        Task<IEnumerable<ExposureControlSettings>> GetAllExposureControlSettingsAsync();

        /// <summary>
        /// Retrieves all exposureControlSettings names from the repsitory.
        /// </summary>
        /// <returns>A <see cref="Task"/> returning the requested experiments names.</returns>
        Task<IEnumerable<string>> GetAllExposureControlSettingsNamesAsync();
    }
}
