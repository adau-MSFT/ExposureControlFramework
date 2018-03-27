namespace ExposureControlFramework.Tests
{
    using System;
    using ExposureControlFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RandomExposureControlSettingsTests
    {
        private RandomExposureControlSettings _exposureControl;

        [TestInitialize]
        public void TestInitialize()
        {
            _exposureControl = new RandomExposureControlSettings("test", DateTime.Today, 100, 1);
        }

        [TestMethod]
        public void WhenAnIdentityIsWithinExposedRangeThenItIsExposed()
        {
            Assert.IsTrue(_exposureControl.IsExposed("ExposedId"));
        }

        [TestMethod]
        public void WhenAnIdentityIsNotWithinExposedRangeThenItIsNotExposed()
        {
            var exposureControl = new RandomExposureControlSettings("test", DateTime.Today, 100, 0);
            Assert.IsFalse(exposureControl.IsExposed("NotExposedId"));
        }
        
        [TestMethod]
        public void WhenAnExposureControlSettingsIsExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new RandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, 0.3);
            Assert.IsTrue(exposureControl.IsExpired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreatingNewExposureSettingsWithNegativeExposureThenExceptionIsThrown()
        {
            var exposureControl = new RandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 100, -0.3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenCreatingNewExposureSettingsWithLargerThan1ExposureThenExceptionIsThrown()
        {
            var exposureControl = new RandomExposureControlSettings("test", DateTime.Today.AddDays(-50), 100, 2.3);
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
