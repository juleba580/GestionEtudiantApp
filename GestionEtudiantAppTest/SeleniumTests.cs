using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace GestionEtudiantAppTest
{
    public class SeleniumTests : IClassFixture<ChromeDriverFixture>
    {
        private readonly IWebDriver _driver;

        public SeleniumTests(ChromeDriverFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void AddUserTest()
        {
            // Navigate to the page where you add a user
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Etudiants/Create");

            // Find the input fields and submit button
            var nomInput = _driver.FindElement(By.Id("Nom"));
            var prenomInput = _driver.FindElement(By.Id("Prenom"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var sexeInput = _driver.FindElement(By.Id("Sexe"));
            var dateNaisInput = _driver.FindElement(By.Id("DateNais"));
            var submitButton = _driver.FindElement(By.Id("submitButton"));

            // Fill in the user information
            nomInput.SendKeys("TestNom");
            prenomInput.SendKeys("TestPrenom");
            emailInput.SendKeys("test@example.com");
            sexeInput.SendKeys("Homme"); // Assuming it's a dropdown
            dateNaisInput.SendKeys("04/22/1990"); // Assuming date format is MM/DD/YYYY

            // Submit the form
            submitButton.Click();

            // Optionally, you can add assertions to check if the user is added successfully
            // For example, you can check if the user appears in the list of users or if a success message is displayed
        }
    }

    public class ChromeDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; }

        public ChromeDriverFixture()
        {
            // Initialize ChromeDriver
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            // Close ChromeDriver
            Driver.Quit();
        }
    }
}
