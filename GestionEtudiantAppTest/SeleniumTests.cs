using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace GestionEtudiantAppTest
{
    public class SeleniumTests : IClassFixture<ChromeDriverFixture>
    {
        private readonly IWebDriver _driver;

        public SeleniumTests(ChromeDriverFixture fixture)
        {
            _driver = fixture.Driver;

            // Navigateur vers la page des �tudiants
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Index");

            // Attendre que la page se charge 
            System.Threading.Thread.Sleep(2000);

            // Log de chargement de la page
            Console.WriteLine("Page charg�e avec succ�s.");

            // Identifier et cliquer sur le bouton "Nouvel �tudiant"
            IWebElement ajoutEtudiantBtn = _driver.FindElement(By.Id("ajoutEtudiant"));
            ajoutEtudiantBtn.Click();
        }

        [Fact]
        public void VerifyUserInformationDisplay()
        {
            // Naviguer vers la page des �tudiants
            _driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Index");

            // Attendre que la page se charge compl�tement
            System.Threading.Thread.Sleep(2000); // Attendre 2 secondes

            // Trouver tous les �l�ments de la table des �tudiants
            var studentRows = _driver.FindElements(By.XPath("//table[@class='table table-striped']//tbody//tr"));

            // V�rifier que la table contient au moins une ligne d'�tudiant
            Assert.NotEmpty(studentRows);

            // V�rifier que chaque ligne d'�tudiant contient des informations non vides
            foreach (var row in studentRows)
            {
                var columns = row.FindElements(By.TagName("td"));
                // S'assurer qu'il y a au moins 5 colonnes pour les informations de l'�tudiant
                Assert.True(columns.Count >= 5); 
                foreach (var column in columns)
                {
                    // V�rifier que le texte de chaque colonne n'est pas vide
                    Assert.False(string.IsNullOrEmpty(column.Text.Trim())); 
                }
            }
        }
    }

    public class ChromeDriverFixture : IDisposable
    {
        public IWebDriver Driver { get; }

        public ChromeDriverFixture()
        {
            try
            {
                // Initialiser ChromeDriver
                Driver = new ChromeDriver();
            }
            catch (Exception e)
            {
                // Log des erreurs
                Console.WriteLine("Erreur : " + e.Message);
                throw;
            }
        }

        public void Dispose()
        {
            // Fermer le navigateur � la fin du test
            Driver.Quit();
        }
    }
}
