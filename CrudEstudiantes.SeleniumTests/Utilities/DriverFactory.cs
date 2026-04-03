using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrudEstudiantes.SeleniumTests.Utilities
{
    public static class DriverFactory
    {
        public static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            return new ChromeDriver(options);
        }
    }
}