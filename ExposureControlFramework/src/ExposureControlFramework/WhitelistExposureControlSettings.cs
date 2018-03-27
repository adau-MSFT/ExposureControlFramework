namespace ExposureControlFramework
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// An <see cref="ExposureControlSettings"/> which controls exposure based on a whitelist.
    /// </summary>
    public class WhitelistExposureControlSettings : ExposureControlSettings
    {
        #region Properties

        /// <summary>
        /// Gets a short string describing the exposure this exposureControlSettings uses.
        /// </summary>
        [JsonIgnore]
        public override string ExposureInfo => $"{this.Whitelist.Count} items";

        /// <summary>
        /// Gets the list of whitelisted applications
        /// </summary>
        public List<string> Whitelist { get; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WhitelistExposureControlSettings"/> class.
        /// </summary>
        /// <param name="name">The name of the exposureControlSettings.</param>
        /// <param name="startDate">The start date of the exposureControlSettings.</param>
        /// <param name="durationInDays">The exposureControlSettings duration in days</param>
        /// <param name="whitelist">The list of whitelisted applications.</param>
        public WhitelistExposureControlSettings(string name, DateTime startDate, int durationInDays, List<string> whitelist)
            : base(name, startDate, durationInDays)
        {
            this.Whitelist = Diagnostics.EnsureArgumentNotNull(() => whitelist);
        }

        #region Overrides of ExposureControlSettings

        /// <summary>
        /// Checks is <paramref name="identity"/> is exposed to this exposureControlSettings.
        /// </summary>
        /// <param name="identity">The application to check.</param>
        /// <returns>
        /// <code>true</code> if <paramref name="identity"/> is exposed to the 
        /// exposureControlSettings, and <code>false</code> otherwise.
        /// </returns>
        public override bool IsExposed(string identity)
        {
            Diagnostics.EnsureStringNotNullOrWhiteSpace(() => identity);

            return this.Whitelist.Contains(identity);
        }

        #endregion
    }
}
