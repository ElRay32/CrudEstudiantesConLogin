using CrudEstudiantes.SeleniumTests.Pages;
using CrudEstudiantes.SeleniumTests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CrudEstudiantes.SeleniumTests.Tests
{
    public class StudentTests
    {
        private IWebDriver? _driver;
        private LoginPage? _loginPage;
        private StudentsPage? _studentsPage;

        [SetUp]
        public void Setup()
        {
            _driver = DriverFactory.CreateChromeDriver();
            _loginPage = new LoginPage(_driver);
            _studentsPage = new StudentsPage(_driver);
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

        private void Login()
        {
            _loginPage!.Navigate();
            _loginPage.Login("admin", "123456");
        }

        [Test]
        public void Create_Student_With_Valid_Data_Should_Save_Successfully()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"juan{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Juan", "Perez", "22", uniqueEmail, "Software");
            _studentsPage.ClickSave();

            var success = _studentsPage.SuccessMessage();

            Assert.That(success, Is.Not.Null);
            Assert.That(success!.Text, Does.Contain("creado correctamente"));
            Assert.That(_studentsPage.IsStudentVisibleInTable("Juan", uniqueEmail), Is.True);
        }

        [Test]
        public void Create_Student_With_Empty_FirstName_Should_Show_Validation_Error()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"nofirstname{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("", "Perez", "22", uniqueEmail, "Software");
            _studentsPage.ClickSave();

            var error = _studentsPage.FirstNameError();

            Assert.That(error, Is.Not.Null);
            Assert.That(error!.Text, Does.Contain("obligatorio"));
        }

        [Test]
        public void Create_Student_With_Invalid_Age_Should_Show_Validation_Error()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"edad{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Carlos", "Lopez", "121", uniqueEmail, "Medicina");
            _studentsPage.ClickSave();

            var error = _studentsPage.AgeError();

            Assert.That(error, Is.Not.Null);
            Assert.That(error!.Text, Does.Contain("entre 1 y 120"));
        }

        [Test]
        public void Student_List_Should_Display_Created_Student()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"list{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Maria", "Gomez", "24", uniqueEmail, "Arquitectura");
            _studentsPage.ClickSave();

            Assert.That(_studentsPage.IsStudentVisibleInTable("Maria", uniqueEmail), Is.True);
        }

        [Test]
        public void Edit_Student_With_Valid_Data_Should_Update_Successfully()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"edit{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Pedro", "Martinez", "21", uniqueEmail, "Derecho");
            _studentsPage.ClickSave();

            Assert.That(_studentsPage.IsStudentVisibleInTable("Pedro", uniqueEmail), Is.True);

            _studentsPage.ClickEditForStudent(uniqueEmail);

            string updatedEmail = $"updated{Guid.NewGuid():N}@test.com";
            _studentsPage.FillEditForm("PedroUpdated", "Martinez", "23", updatedEmail, "Psicologia");
            _studentsPage.ClickUpdate();

            var success = _studentsPage.SuccessMessage();

            Assert.That(success, Is.Not.Null);
            Assert.That(success!.Text, Does.Contain("actualizado correctamente"));
            Assert.That(_studentsPage.IsStudentVisibleInTable("PedroUpdated", updatedEmail), Is.True);
        }

        [Test]
        public void Edit_Student_With_Invalid_Email_Should_Show_Validation_Error()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"editinvalid{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Laura", "Ruiz", "20", uniqueEmail, "Marketing");
            _studentsPage.ClickSave();

            _studentsPage.ClickEditForStudent(uniqueEmail);
            _studentsPage.FillEditForm("Laura", "Ruiz", "20", "correo_invalido", "Marketing");
            _studentsPage.ClickUpdate();

            var error = _studentsPage.EmailError();

            Assert.That(error, Is.Not.Null);
            Assert.That(error!.Text, Does.Contain("válido"));
        }

        [Test]
        public void Delete_Student_Should_Remove_Student_From_List()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"delete{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Jose", "Santana", "25", uniqueEmail, "Contabilidad");
            _studentsPage.ClickSave();

            Assert.That(_studentsPage.IsStudentVisibleInTable("Jose", uniqueEmail), Is.True);

            _studentsPage.ClickDeleteForStudent(uniqueEmail);
            _studentsPage.ConfirmDelete();

            var success = _studentsPage.SuccessMessage();

            Assert.That(success, Is.Not.Null);
            Assert.That(success!.Text, Does.Contain("eliminado correctamente"));
            Assert.That(_studentsPage.IsTextPresent(uniqueEmail), Is.False);
        }

        [Test]
        public void Cancel_Delete_Should_Keep_Student_In_List()
        {
            Login();
            _studentsPage!.ClickCreateStudent();

            string uniqueEmail = $"canceldelete{Guid.NewGuid():N}@test.com";

            _studentsPage.FillCreateForm("Ana", "Lopez", "26", uniqueEmail, "Medicina");
            _studentsPage.ClickSave();

            Assert.That(_studentsPage.IsStudentVisibleInTable("Ana", uniqueEmail), Is.True);

            _studentsPage.ClickDeleteForStudent(uniqueEmail);
            _studentsPage.CancelDelete();

            Assert.That(_studentsPage.IsTextPresent(uniqueEmail), Is.True);
        }
    }
}