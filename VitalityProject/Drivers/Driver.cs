using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using System.Configuration;

namespace VitalityProject.Drivers
{
    public class Driver
    {
        public IWebDriver driver;

        public IWebDriver OpenBrowser(string browser= "chrome")
        {
           driver = new ChromeDriver();
           driver.Manage().Window.Maximize();
           return driver;
        }
    }
}
