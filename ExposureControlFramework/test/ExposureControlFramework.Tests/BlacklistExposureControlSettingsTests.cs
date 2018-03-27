namespace ExposureControlFramework.Tests
{
    using System;
    using System.Collections.Generic;
    using ExposureControlFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlacklistExposureControlSettingsTests
    {
        private BlacklistExposureControlSettings _exposureControl;

        [TestInitialize]
        public void TestInitialize()
        {
            _exposureControl = new BlacklistExposureControlSettings("test", DateTime.Today, 100, new List<string>(){"blacklistedId", "anotherBlacklistedId"});
        }

        [TestMethod]
        public void WhenAnIdentityIsBlacklistThenItIsNotExposed()
        {
            Assert.IsFalse(_exposureControl.IsExposed("blacklistedId"));
        }

        [TestMethod]
        public void WhenAnIdentityIsNotBlacklistThenItIsExposed()
        {
            Assert.IsTrue(_exposureControl.IsExposed("notBlacklistedId"));
        }

        [TestMethod]
        public void WhenAnExposureControlSettingsIsNotExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new BlacklistExposureControlSettings("test", DateTime.Today.AddDays(-5), 10, new List<string>() { "blacklistedId", "anotherBlacklistedId" });
            Assert.IsFalse(exposureControl.IsExpired);
        }

        [TestMethod]
        public void WhenAnExposureControlSettingsIsExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new BlacklistExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, new List<string>() { "blacklistedId", "anotherBlacklistedId" });
            Assert.IsTrue(exposureControl.IsExpired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCreatingNewExposureSettingsWithNullListThenExceptionIsThrown()
        {
            var exposureControl = new BlacklistExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, null);
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
