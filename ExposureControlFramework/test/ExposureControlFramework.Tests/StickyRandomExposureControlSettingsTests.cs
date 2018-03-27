namespace ExposureControlFramework.Tests
{
    using System;
    using ExposureControlFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StickyRandomExposureControlSettingsTests
    {
        private StickyRandomExposureControlSettings _exposureControl;

        [TestInitialize]
        public void TestInitialize()
        {
            _exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today, 100, 0.2, 70);
        }

        [TestMethod]
        public void WhenAnIdentityIsWithinExposedRangeThenItIsExposed()
        {
            Assert.IsTrue(_exposureControl.IsExposed("ExposedId"));
        }

        [TestMethod]
        public void WhenAnIdentityIsNotWithinExposedRangeThenItIsNotExposed()
        {
            Assert.IsFalse(_exposureControl.IsExposed("NotExposedId"));
        }

        [TestMethod]
        public void WhenSkewCausesExperimentToGoOverTheLimitThenTheRangeIsShifted()
        {
            var exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today, 100, 0.5, 90);
            Assert.IsTrue(exposureControl.IsExposed("AnIdToExpose"));
        }

        [TestMethod]
        public void WhenAnExposureControlSettingsIsExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, 0.3, 0);
            Assert.IsTrue(exposureControl.IsExpired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreatingNewExposureSettingsWithNegativeExposureThenExceptionIsThrown()
        {
            var exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, -0.3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreatingNewExposureSettingsWithLargerThan1ExposureThenExceptionIsThrown()
        {
            var exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, 2.3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreatingNewExposureSettingsWithSkewLargerThan99ThenExceptionIsThrown()
        {
            var exposureControl = new StickyRandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, 2.3, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCheckingIfExposedWithNullValueExceptionIsThrown()
        {
            _exposureControl.IsExposed(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCheckingIfExposedWithEmptyValueExceptionIsThrown()
        {
            _exposureControl.IsExposed("");
        }
    }
}
