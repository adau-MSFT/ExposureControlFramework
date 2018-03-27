namespace ExposureControlFramework
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// An <see cref="ExposureControlSettings"/> which controls exposure based on pure random
    /// decision
    /// </summary>
    public class RandomExposureControlSettings : ExposureControlSettings
    {
        #region Properties

        /// <summary>
        /// Gets a short string describing the exposure this exposureControlSettings uses.
        /// </summary>
        [JsonIgnore]
        public override string ExposureInfo => $"{(this.ExposureChance * 100):N0}%";

        /// <summary>
        /// Gets the chance for an application to be exposed.
        /// This should be a number between 0 and 1.
        /// </summary>
        public double ExposureChance { get; }

        private Random _random { get; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomExposureControlSettings"/> class.
        /// </summary>
        /// <param name="name">The name of the exposureControlSettings</param>
        /// <param name="startDate">The start date of the exposureControlSettings.</param>
        /// <param name="durationInDays">The exposureControlSettings duration in days</param>
        /// <param name="exposureChance">The exposureControlSettings exposure chance</param>
        public RandomExposureControlSettings(string name, DateTime startDate, int durationInDays, double exposureChance) 
            : base(name, startDate, durationInDays)
        {
            this.ExposureChance = Diagnostics.EnsureArgumentInRange(() => exposureChance, 0, 1);
            this._random = new Random();
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
                       
            return this._random.NextDouble() < this.ExposureChance;
        }

        #endregion
    }
}
