using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace GestionEtudiantAppTest
{
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
            // Verify if the page contains the text "Liste des étudiants"
                Assert.Contains("Liste des étudiants", driver.PageSource);

            // Trouver tous les éléments de la table des étudiants
          //  var studentRows = driver.FindElements(By.CssSelector("table.table.table-striped tbody tr"));

            // Vérifier que la table contient au moins une ligne d'étudiant
           // Assert.NotEmpty(studentRows);

            // Vérifier que chaque ligne d'étudiant contient des informations non vides
            

            // Fermer le navigateur
            driver.Quit();
        }
    }
}
