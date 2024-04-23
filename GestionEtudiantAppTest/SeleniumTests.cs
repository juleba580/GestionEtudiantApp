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
        }

        [Fact]
        public void VerifyStudentListPageDisplaysCorrectly()
        {
            // Naviguer vers la page des étudiants
            _driver.Navigate().GoToUrl("URL_de_votre_page_des_etudiants");

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
        }
    }
}
