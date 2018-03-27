namespace ExposureControlFramework
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// A class representing an exposureControlSettings, controling the exposure of some
    /// feature.
    /// </summary>
    public abstract class ExposureControlSettings
    {
        protected ExposureControlSettings()
        {
        }

        /// <summary>
        /// Gets the name of the exposureControlSettings.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the start date of the exposureControlSettings.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the exposureControlSettings duration in days.
        /// </summary>
        public int DurationInDays { get; }

        /// <summary>
        /// Gets the exposureControlSettings expiration date.
        /// </summary>
        [JsonIgnore]
        public DateTime ExpirationDate => this.StartDate.Date.AddDays(this.DurationInDays);

        /// <summary>
        /// Gets a value indicating whether the exposureControlSettings has expired.
        /// </summary>
        [JsonIgnore]
        public bool IsExpired => this.ExpirationDate < DateTime.UtcNow.Date;

        /// <summary>
        /// Gets a short string describing the exposure this exposureControlSettings uses.
        /// </summary>
        [JsonIgnore]
        public abstract string ExposureInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExposureControlSettings"/> class.
        /// </summary>
        /// <param name="name">The name of the exposureControlSettings.</param>
        /// <param name="startDate">The start date of the exposureControlSettings.</param>
        /// <param name="durationInDays">The exposureControlSettings duration in days</param>
        protected ExposureControlSettings(string name, DateTime startDate, int durationInDays)
        {
            Diagnostics.EnsureStringNotNullOrWhiteSpace(() => name);
            Diagnostics.EnsureArgument(!name.Any(Char.IsWhiteSpace), () => name, "ExposureControlSettings name cannot contain whitespaces");

            this.Name = name;
            this.StartDate = startDate;
            this.DurationInDays = Diagnostics.EnsureArgumentInRange(() => durationInDays, 1, int.MaxValue);
        }

        /// <summary>
        /// Checks is <paramref name="identity"/> is exposed to this exposureControlSettings.
        /// </summary>
        /// <param name="identity">The identity to check.</param>
        /// <returns>
        /// <code>true</code> if <paramref name="identity"/> is exposed to the 
        /// exposureControlSettings, and <code>false</code> otherwise.
        /// </returns>
        public abstract bool IsExposed(string identity);
    }
}
