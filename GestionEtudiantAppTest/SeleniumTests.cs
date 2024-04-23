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
            // Naviguer vers la page des étudiants
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Index");

            // Attendre que la page se charge complètement
            System.Threading.Thread.Sleep(2000); // Attendre 2 secondes (à remplacer par une attente explicite si possible)

            // Trouver tous les éléments de la table des étudiants
            var studentRows = _driver.FindElements(By.XPath("//table[@class='table table-striped']//tbody//tr"));

            // Vérifier que la table contient au moins une ligne d'étudiant
            Assert.NotEmpty(studentRows);

            // Vérifier que chaque ligne d'étudiant contient des informations non vides
            foreach (var row in studentRows)
            {
                var columns = row.FindElements(By.TagName("td"));
                Assert.True(columns.Count >= 5); // S'assurer qu'il y a au moins 5 colonnes pour les informations de l'étudiant
                foreach (var column in columns)
                {
                    Assert.False(string.IsNullOrEmpty(column.Text.Trim())); // Vérifier que le texte de chaque colonne n'est pas vide
                }
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
