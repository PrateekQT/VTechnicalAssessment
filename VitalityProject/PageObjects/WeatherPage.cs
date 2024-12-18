using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace VitalityProject.PageObjects
{
    public class WeatherPage
    {
        private IWebDriver _driver;
        WebDriverWait wait;
        public WeatherPage(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(5000));
        }

        private IWebElement searchbox => _driver.FindElement(By.Id("ls-c-search__input-label"));
        private List<IWebElement> searchList => _driver.FindElements(By.CssSelector("li.ls-c-locations-list-item")).ToList();
        private IWebElement cityName => _driver.FindElement(By.Id("wr-location-name-id"));
        private IWebElement tomorrowDateLink => _driver.FindElement(By.Id("daylink-1"));
        private List<IWebElement> temperature => _driver.FindElements(By.XPath("//a[@id= 'daylink-1']//span[@class= 'wr-value--temperature--c']")).ToList();

        private IWebElement MaxTemperature(string day)
        {
            return _driver.FindElement(By.CssSelector($".wr-day--{day} .wr-day-temperature__high-value .wr-value--temperature--c"));
        }

        private IWebElement MinTemperature(string day)
        {
            return _driver.FindElement(By.CssSelector($".wr-day--{day} .wr-day-temperature__low-value .wr-value--temperature--c"));
        }

        public void SearchCity(string cityName)
        {
            searchbox.SendKeys(cityName);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            searchList.First().Click();
        }

        public void SelectTomorrowsDate()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(tomorrowDateLink);
            
            wait.Until(ExpectedConditions.ElementToBeClickable(tomorrowDateLink));
            tomorrowDateLink.Click();
        }

        public string GetCurrentCityName()
        {
            return cityName.Text;
        }

        public int GetMaximumTemperature(string day)
        {
            var max= MaxTemperature(day).Text;
            max= max.Remove(max.Length-1);
            return Convert.ToInt32(max);
        }
        public int GetMinimumTemperature(string day)
        {
            var min= MinTemperature(day).Text;
            min= min.Remove(min.Length-1);
            return Convert.ToInt32(min);
        }
    }
}
