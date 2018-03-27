namespace ExposureControlFramework.Tests
{
    using System;
    using System.Collections.Generic;
    using ExposureControlFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WhitelistExposureControlSettingsTests
    {
        private WhitelistExposureControlSettings _exposureControl;

        [TestInitialize]
        public void TestInitialize()
        {
            _exposureControl = new WhitelistExposureControlSettings("test", DateTime.Today, 100, new List<string>(){"whitelisttedId", "anotherWhitelistetdId"});
        }

        [TestMethod]
        public void WhenAnIdentityIsWhitelisttedThenItIsExposed()
        {
            Assert.IsTrue(_exposureControl.IsExposed("whitelisttedId"));
        }

        [TestMethod]
        public void WhenAnIdentityIsNotWhitelisttedThenItIsNotExposed()
        {
            Assert.IsFalse(_exposureControl.IsExposed("notWhiteklisttedId"));
        }

        [TestMethod]
        public void WhenAnExposureControlSettingsIsNotExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new WhitelistExposureControlSettings("test", DateTime.Today.AddDays(-5), 10, new List<string>() { "blacklistedId", "anotherBlacklistedId" });
            Assert.IsFalse(exposureControl.IsExpired);
        }

        [TestMethod]
        public void WhenAnExposureControlSettingsIsExpiredThenTheResultIsCorrect()
        {
            var exposureControl = new WhitelistExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, new List<string>() { "blacklistedId", "anotherBlacklistedId" });
            Assert.IsTrue(exposureControl.IsExpired);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCreatingNewExposureSettingsWithNullListThenExceptionIsThrown()
        {
            var exposureControl = new WhitelistExposureControlSettings("test", DateTime.Today.AddDays(-50), 10, null);
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