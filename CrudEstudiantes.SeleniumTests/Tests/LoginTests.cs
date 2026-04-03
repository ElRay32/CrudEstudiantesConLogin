using CrudEstudiantes.SeleniumTests.Pages;
using CrudEstudiantes.SeleniumTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CrudEstudiantes.SeleniumTests.Tests
{
    public class LoginTests
    {
        private IWebDriver? _driver;
        private LoginPage? _loginPage;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateChromeDriver();
            _loginPage = new LoginPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                ScreenshotHelper.TakeScreenshot(_driver, TestContext.CurrentContext.Test.Name);
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [Test]
        public void Login_With_Valid_Credentials_Should_Redirect_To_Students_Page()
        {
            _loginPage!.Navigate();
            _loginPage.Login("admin", "123456");

            Assert.That(_driver!.Url, Does.Contain("/Students"));
        }

        [Test]
        public void Login_With_Invalid_Credentials_Should_Show_Error_Message()
        {
            _loginPage!.Navigate();
            _loginPage.Login("admin", "000000");

            var error = _loginPage.LoginError();

            Assert.That(error, Is.Not.Null);
            Assert.That(error!.Text, Does.Contain("incorrectos"));
        }

        [Test]
        public void Login_With_Empty_Fields_Should_Stay_On_Login_Page()
        {
            _loginPage!.Navigate();
            _loginPage.ClickLogin();

            Assert.That(_driver!.Url, Does.Contain("/Account/Login"));
        }
    }
}