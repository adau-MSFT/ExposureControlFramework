namespace ExposureControlFramework.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AppSettingsExposureControlSettingsReaderTests
    {
        private readonly AppSettingsExposureControlSettingsReader _exposureControlSettingsReader;

        public AppSettingsExposureControlSettingsReaderTests()
        {
            _exposureControlSettingsReader = new AppSettingsExposureControlSettingsReader();
        }

        [TestMethod]
        public async Task WhenNoExposureControlSettingsExistsWithTheRequestedNameThenNullIsReturned()
        {            
            var result = await _exposureControlSettingsReader.GetExposureControlSettingsAsync<RandomExposureControlSettings>("notexisting");
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public async Task WhenAnExposureControlSettingsExistsWithTheRequestedNameThenItIsReturned()
        {
            var result = await _exposureControlSettingsReader.GetExposureControlSettingsAsync<RandomExposureControlSettings>("existing");
            Assert.IsTrue(result != null);
            Assert.AreEqual(new DateTime(2018, 02, 19), result.StartDate, $"Start date is wrong expected {new DateTime(2018, 02, 19)} got {result.StartDate}");
            Assert.AreEqual(100, result.DurationInDays, $"Duration in days is wrong. Expected 100 got {result.DurationInDays}");
            Assert.AreEqual(0.5, result.ExposureChance, $"exposure chance is wrong. Expected 0.5 got {result.ExposureChance}");
        }
    }
}
