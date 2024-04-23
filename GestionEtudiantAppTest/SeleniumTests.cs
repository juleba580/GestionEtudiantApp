using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class SeleniumTest
{
    static void Main(string[] args)
    {
        // Configuration du navigateur Chrome
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized"); // Maximise la fenêtre du navigateur
        IWebDriver driver = new ChromeDriver(options);

        try
        {
            // Navigateur vers la page des étudiants
            driver.Navigate().GoToUrl("https://localhost:7173/Etudiants/Index");

            // Attendre que la page se charge (vous pouvez ajuster le délai selon vos besoins)
            System.Threading.Thread.Sleep(2000);

            // Log de chargement de la page
            Console.WriteLine("Page chargée avec succès.");

            // Identifier et cliquer sur le bouton "Nouvel étudiant"
            IWebElement ajoutEtudiantBtn = driver.FindElement(By.Id("ajoutEtudiant"));
            ajoutEtudiantBtn.Click();

            // Log d'identification et de clic sur le bouton "Nouvel étudiant"
            Console.WriteLine("Bouton 'Nouvel étudiant' cliqué.");

            // Autres étapes du test...

        }
        catch (Exception e)
        {
            // Log des erreurs
            Console.WriteLine("Erreur : " + e.Message);
        }
        finally
        {
            // Fermer le navigateur à la fin du test
            driver.Quit();
        }
    }
}
