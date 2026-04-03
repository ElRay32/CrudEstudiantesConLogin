using OpenQA.Selenium;

namespace CrudEstudiantes.SeleniumTests.Pages
{
    public class StudentsPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = "http://localhost:5000/Students/Index";

        public StudentsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement CreateStudentButton => _driver.FindElement(By.Id("create-student-button"));
        private IWebElement FirstNameInput => _driver.FindElement(By.Id("first-name"));
        private IWebElement LastNameInput => _driver.FindElement(By.Id("last-name"));
        private IWebElement AgeInput => _driver.FindElement(By.Id("age"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        private IWebElement MajorInput => _driver.FindElement(By.Id("major"));
        private IWebElement SaveStudentButton => _driver.FindElement(By.Id("save-student-button"));
        private IWebElement UpdateStudentButton => _driver.FindElement(By.Id("update-student-button"));
        private IWebElement ConfirmDeleteButton => _driver.FindElement(By.Id("confirm-delete-button"));
        private IWebElement CancelDeleteButton => _driver.FindElement(By.Id("cancel-delete-button"));

        public IWebElement? SuccessMessage()
        {
            var elements = _driver.FindElements(By.Id("success-message"));
            return elements.Count > 0 ? elements[0] : null;
        }

        public IWebElement? FirstNameError()
        {
            var elements = _driver.FindElements(By.Id("first-name-error"));
            return elements.Count > 0 ? elements[0] : null;
        }

        public IWebElement? EmailError()
        {
            var elements = _driver.FindElements(By.Id("email-error"));
            return elements.Count > 0 ? elements[0] : null;
        }

        public IWebElement? AgeError()
        {
            var elements = _driver.FindElements(By.Id("age-error"));
            return elements.Count > 0 ? elements[0] : null;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
        }

        public void ClickCreateStudent()
        {
            CreateStudentButton.Click();
        }

        public void FillCreateForm(string firstName, string lastName, string age, string email, string major)
        {
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(firstName);

            LastNameInput.Clear();
            LastNameInput.SendKeys(lastName);

            AgeInput.Clear();
            AgeInput.SendKeys(age);

            EmailInput.Clear();
            EmailInput.SendKeys(email);

            MajorInput.Clear();
            MajorInput.SendKeys(major);
        }

        public void ClickSave()
        {
            SaveStudentButton.Click();
        }

        public void FillEditForm(string firstName, string lastName, string age, string email, string major)
        {
            FirstNameInput.Clear();
            FirstNameInput.SendKeys(firstName);

            LastNameInput.Clear();
            LastNameInput.SendKeys(lastName);

            AgeInput.Clear();
            AgeInput.SendKeys(age);

            EmailInput.Clear();
            EmailInput.SendKeys(email);

            MajorInput.Clear();
            MajorInput.SendKeys(major);
        }

        public void ClickUpdate()
        {
            UpdateStudentButton.Click();
        }

        public bool IsStudentVisibleInTable(string firstName, string email)
        {
            return _driver.PageSource.Contains(firstName) && _driver.PageSource.Contains(email);
        }

        public void ClickEditForStudent(string email)
        {
            var editButton = _driver.FindElement(By.XPath($"//tr[td[contains(text(),'{email}')]]//a[contains(@id,'edit-student-')]"));
            editButton.Click();
        }

        public void ClickDeleteForStudent(string email)
        {
            var deleteButton = _driver.FindElement(By.XPath($"//tr[td[contains(text(),'{email}')]]//a[contains(@id,'delete-student-')]"));
            deleteButton.Click();
        }

        public void ConfirmDelete()
        {
            ConfirmDeleteButton.Click();
        }

        public void CancelDelete()
        {
            CancelDeleteButton.Click();
        }

        public bool IsTextPresent(string text)
        {
            return _driver.PageSource.Contains(text);
        }
    }
}