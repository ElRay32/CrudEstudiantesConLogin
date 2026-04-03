using OpenQA.Selenium;

namespace CrudEstudiantes.SeleniumTests.Utilities
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            if (driver is ITakesScreenshot screenshotDriver)
            {
                var projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                var folderPath = Path.Combine(projectPath, "Screenshots");

                Directory.CreateDirectory(folderPath);

                var safeTestName = string.Concat(testName.Split(Path.GetInvalidFileNameChars()));
                var fileName = $"{safeTestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var fullPath = Path.Combine(folderPath, fileName);

                var screenshot = screenshotDriver.GetScreenshot();
                screenshot.SaveAsFile(fullPath);
            }
        }
    }
}