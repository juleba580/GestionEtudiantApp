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
            // Navigat to my add page user
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Etudiants/Create");

            // Recherchez les champs de saisie et le bouton Soumettre
            var nomInput = _driver.FindElement(By.Id("Nom"));
            var prenomInput = _driver.FindElement(By.Id("Prenom"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var sexeInput = _driver.FindElement(By.Id("Sexe"));
            var dateNaisInput = _driver.FindElement(By.Id("DateNais"));
            var submitButton = _driver.FindElement(By.Id("submitButton"));

            // Remplire les informations utilisateur
            nomInput.SendKeys("TestSouley");
            prenomInput.SendKeys("TestBa");
            emailInput.SendKeys("souleyt@galbeme.com");
            sexeInput.SendKeys("Homme");
            dateNaisInput.SendKeys("04/22/1990");

            // Soumettre le formulaire
            submitButton.Click();

            // si l'utilisateur est ajouté avec succès
            var successMessage = _driver.FindElement(By.Id("successMessage"));
            Assert.NotNull(successMessage);
            Assert.Contains("User added successfully", successMessage.Text);

           // vérifier si l'utilisateur apparaît dans la liste des utilisateurs
            var userList = _driver.FindElement(By.Id("userList"));
            Assert.Contains("TestNom", userList.Text);
            Assert.Contains("TestPrenom", userList.Text);
            Assert.Contains("test@example.com", userList.Text);

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
