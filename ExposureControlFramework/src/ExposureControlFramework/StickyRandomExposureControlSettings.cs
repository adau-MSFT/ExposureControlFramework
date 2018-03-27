namespace ExposureControlFramework
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// An <see cref="ExposureControlSettings"/> which controls exposure based on a psuedo random decision,
    /// which is based on the exposureControlSettings size.
    /// </summary>
    public class StickyRandomExposureControlSettings : ExposureControlSettings
    {
        #region Properties

        /// <summary>
        /// Gets a short string describing the exposure this exposureControlSettings uses.
        /// </summary>
        [JsonIgnore]
        public override string ExposureInfo => $"{(this.ExperimentSize * 100):N0}% ({this.HashSkew})";


        /// <summary>
        /// Gets the size of the exposureControlSettings (in percentage).
        /// This should be a number between 0 and 1.
        /// </summary>
        public double ExperimentSize { get; }

        /// <summary>
        /// Gets a hash skew for the exposureControlSettings. 
        /// Hash skew is between 0 and 100.
        /// This is used to skew the scope of exposure in a consistant way throughout the lifetime of the exposureControlSettings.
        /// This allows you to define several sticky experiments on the same exposure property without them overlapping.
        /// </summary>
        public int HashSkew { get; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="StickyRandomExposureControlSettings"/> class.
        /// </summary>
        /// <param name="name">The name of the exposureControlSettings.</param>
        /// <param name="startDate">The start date of the experiment.</param>
        /// <param name="durationInDays">The exposureControlSettings duration in days</param>
        /// <param name="scope">The exposure scope.</param>
        /// <param name="experimentSize">The exposureControlSettings size.</param>
        /// <param name="hashSkew">The hash skew for the exposureControlSettings.</param>
        [JsonConstructor]
        public StickyRandomExposureControlSettings(string name, DateTime startDate, int durationInDays, double experimentSize, int hashSkew)
            : base(name, startDate, durationInDays)
        {
            this.ExperimentSize = Diagnostics.EnsureArgumentInRange(() => experimentSize, 0, 1);
            this.HashSkew = Diagnostics.EnsureArgumentInRange(() => hashSkew, 0, 99);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickyRandomExposureControlSettings"/> class based on a <see cref="RandomExposureControlSettings"/>.
        /// </summary>
        /// <param name="randomExposureControlSettings">The random exposureControlSettings to base this sticky exposureControlSettings on.</param>
        /// <param name="hashSkew">The hash skew for the exposureControlSettings.</param>
        public StickyRandomExposureControlSettings(RandomExposureControlSettings randomExposureControlSettings, int hashSkew)
            : base(randomExposureControlSettings.Name, randomExposureControlSettings.StartDate, randomExposureControlSettings.DurationInDays)
        {
            this.ExperimentSize = randomExposureControlSettings.ExposureChance;
            this.HashSkew = Diagnostics.EnsureArgumentInRange(() => hashSkew, 0, 99);
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

            int exposurePropertyHash = identity.GetHashCode();

            // Get the correct exposure property value for the calculated hash
            double exposurePropertyValue = Math.Abs(exposurePropertyHash) % 100;

            // Handle exposure property value as a cyclic range of values to allow exposing the correct
            // number of applications under certain exposureControlSettings size and skew configuration.
            // Example: Skew size of 80 and requested exposureControlSettings size of 0.5. 
            //          We want to expose applications that have a module value X where 80 < X < 100 (20%) or 0 <= X < 30 (another 30%)
            // This is to ensure that the correct exposureControlSettings size is used even when skewing is configured 
            double topLimit = this.HashSkew + this.ExperimentSize * 100;

            if (topLimit > 100)
            {
                return (this.HashSkew <= exposurePropertyValue && exposurePropertyValue < 100) || (0 <= exposurePropertyValue && exposurePropertyValue < topLimit % 100);
            } 

            return this.HashSkew <= exposurePropertyValue && exposurePropertyValue < topLimit;
        }

        /// <summary>
        /// Checks if exposed to all (100% exposure).
        /// </summary>
        /// <returns>
        /// <code>true</code> if is exposed to all
        ///  and <code>false</code> otherwise.
        /// </returns>
        public bool IsExposedToAll()
        {
            return double.Equals (this.ExperimentSize, 1);
        }

        #endregion
    }
}
