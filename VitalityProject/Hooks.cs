using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using VitalityProject.Drivers;

namespace VitalityProject
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;
        private IWebDriver _driver;
        Driver driver = new Driver();
        public Hooks(IObjectContainer container)
        {
            _container = container;
        }
        [BeforeScenario]
        public void InitializeDriver()
        {
            _driver = driver.OpenBrowser();
            _container.RegisterInstanceAs(_driver);
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _driver.Quit();
        }

        public void TakeScreenshot()
        {
            string timestamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            string dir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "");
            DirectoryInfo di = Directory.CreateDirectory(dir + "\\Screenshot\\" + timestamp + "");
            string screenShotPath = dir + "Screenshot\\" + timestamp + "\\test" + ".png";
            Screenshot screenShot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenShot.SaveAsFile(screenShotPath);
        }
    }
}