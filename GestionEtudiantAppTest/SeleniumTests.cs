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
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Etudiants/create");

            // Localiser les champs de saisie et le bouton Soumettre

            var nomInput = _driver.FindElement(By.XPath("//input[@id='Nom']"));
            // Afficher le résultat dans une boîte de dialogue
            System.Windows.Forms.MessageBox.Show(nomInput.Text);
            var prenomInput = _driver.FindElement(By.XPath("//input[@id='Prenom']"));
            var emailInput = _driver.FindElement(By.XPath("//input[@id='Email']"));
            var sexeInput = _driver.FindElement(By.XPath("//input[@id='Sexe']"));
            var dateNaisInput = _driver.FindElement(By.XPath("//input[@id='DateNais']"));
            var submitButton = _driver.FindElement(By.XPath("//button[@type='submit']"));


            // Remplire les informations utilisateur
            nomInput.SendKeys("TestSouley");
            prenomInput.SendKeys("TestBa");
            emailInput.SendKeys("souleyt@galbeme.com");
            sexeInput.SendKeys("Homme");
            dateNaisInput.SendKeys("04/02/1990");

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
