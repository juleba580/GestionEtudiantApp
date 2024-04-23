<<<<<<< HEAD
﻿using OpenQA.Selenium;
=======
using OpenQA.Selenium;
>>>>>>> 11dd71d77425fe89c60438b9959d78ce8e42976a
using OpenQA.Selenium.Chrome;
using Xunit;

namespace GestionEtudiantAppTest
{
<<<<<<< HEAD
    public class SeleniumTests
    {
        [Fact]
        public void VerifyStudentsDisplay()
        {
            // Configuration du pilote Chrome
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Pour exécuter le navigateur en mode headless (sans interface graphique)
            IWebDriver driver = new ChromeDriver(options);

            // Navigation vers la page des étudiants
            driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Index");

            // Attendre que la page se charge complètement
            System.Threading.Thread.Sleep(2000); // Attendre 2 secondes (à remplacer par une attente explicite si possible)

            // Trouver tous les éléments de la table des étudiants
            var studentRows = driver.FindElements(By.XPath("//table[@class='table table-striped']//tbody//tr"));

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

            // Fermer le navigateur
            driver.Quit();
=======
    public class SeleniumTests : IClassFixture<ChromeDriverFixture>
    {
        private readonly IWebDriver _driver;

        public SeleniumTests(ChromeDriverFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void VerifyStudentListPageDisplaysCorrectly()
        {
            // Naviguer vers la page des étudiants
            _driver.Navigate().GoToUrl("http1://localhost:7173/Etudiants/Index");

            // Vérifier que la page est chargée en vérifiant le titre ou d'autres éléments distinctifs
            Assert.Contains("Liste des étudiants", _driver.Title);

            // Vérifier la présence de la table des étudiants
            IWebElement table = _driver.FindElement(By.ClassName("table-striped"));
            Assert.NotNull(table);

            // Vérifier que la table contient des données d'étudiants
            IWebElement tbody = table.FindElement(By.TagName("tbody"));
            Assert.NotEmpty(tbody.FindElements(By.TagName("tr")));
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
>>>>>>> 11dd71d77425fe89c60438b9959d78ce8e42976a
        }
    }
}
