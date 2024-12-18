using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using VitalityProject.PageObjects;

namespace VitalityProject.StepDefinitions
{
    [Binding]
    public class BbcWeatherSteps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly WeatherPage _weatherPage;
        private string url = "https://www.bbc.co.uk/weather";

        public BbcWeatherSteps(ScenarioContext scenarioContext, IWebDriver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
            _weatherPage = new WeatherPage(_driver);
        }

        [Given(@"User open the BBC weather application")]
        public void GivenUserOpenTheBBCNewsApplication()
        {
            _driver.Navigate().GoToUrl(url);
        }

        [When(@"User searches the ""([^""]*)"" city")]
        public void WhenUserSearchesTheCity(string cityName)
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(40);
            _weatherPage.SearchCity(cityName);
            _scenarioContext.Add("CityName", cityName);
        }

        [Then(@"verifies that the maximum temperature is greater than minimum temperature")]
        public void ThenVerifiesThatTheMaximumTemperatureIsGreaterThanMinimumTemperature()
        {
            var cityNameExpected = _scenarioContext.Get<string>("CityName");
            var cityNameActual = _weatherPage.GetCurrentCityName();
            Assert.AreEqual(cityNameActual, cityNameExpected, "Different city is opened than expected");

            _weatherPage.SelectTomorrowsDate();
            int maxTemp = _weatherPage.GetMaximumTemperature("1");
            int minTemp = _weatherPage.GetMinimumTemperature("1");

            Assert.IsTrue(maxTemp > minTemp, "Maximum temperature is not greater than minimum temperature");
        }
    }
}
