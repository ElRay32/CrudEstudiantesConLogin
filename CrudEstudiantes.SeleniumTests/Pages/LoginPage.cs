using OpenQA.Selenium;

namespace CrudEstudiantes.SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5000/Account/Login";

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameInput => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        private IWebElement LoginTitle => _driver.FindElement(By.Id("login-title"));

        public IWebElement? LoginError()
        {
            var elements = _driver.FindElements(By.Id("login-error"));
            return elements.Count > 0 ? elements[0] : null;
        }

        public string GetTitleText()
        {
            return LoginTitle.Text;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
        }

        public void Login(string username, string password)
        {
            UsernameInput.Clear();
            UsernameInput.SendKeys(username);

            PasswordInput.Clear();
            PasswordInput.SendKeys(password);

            LoginButton.Click();
        }

        public void ClickLogin()
        {
            LoginButton.Click();
        }
    }
}
