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
        public void VerifyUserInformationDisplay()
        {
            // Naviguer vers la page d'accueil des étudiants
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants");

            // Attendre que la page soit complètement chargée
            System.Threading.Thread.Sleep(2000); // Temps d'attente pour permettre le chargement de la page

            // Récupérer tous les éléments de la table
            var userRows = _driver.FindElements(By.XPath("//table[@class='table table-striped']//tbody//tr"));

            // Vérifier si des utilisateurs sont présents
            Assert.NotEmpty(userRows);

            // Parcourir chaque ligne de la table pour vérifier les informations de l'utilisateur
            foreach (var row in userRows)
            {
                var columns = row.FindElements(By.TagName("td"));
                Assert.Equal(5, columns.Count); // Vérifier si chaque utilisateur a 5 colonnes

                // Vérifier si les informations de l'utilisateur sont correctes
                Assert.False(string.IsNullOrEmpty(columns[0].Text)); // Nom
                Assert.False(string.IsNullOrEmpty(columns[1].Text)); // Prénom
                Assert.False(string.IsNullOrEmpty(columns[2].Text)); // Email
                Assert.False(string.IsNullOrEmpty(columns[3].Text)); // Sexe
                Assert.False(string.IsNullOrEmpty(columns[4].Text)); // Date de Naissance
            }
        }
    }

    public class ChromeDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; }

        public ChromeDriverFixture()
        {
            // Initialiser ChromeDriver
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            // Fermer ChromeDriver
            Driver.Quit();
        }
    }
}
