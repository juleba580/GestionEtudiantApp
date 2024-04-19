using Microsoft.EntityFrameworkCore;
using GestionEtudiantApp.Models;

namespace GestionEtudiantApp.Data
{
    public class SeedDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GestionEtudiantAppDbContext(serviceProvider.GetRequiredService<DbContextOptions<GestionEtudiantAppDbContext>>()))
            {
                // Check any etudiant exists 
                if (context.Etudiants.Any())
                {
                    return; // Data already exists no need to generate
                }

                context.Etudiants.AddRange(
                    new Etudiant
                    {
                        Nom = "Doe",
                        Prenom = "John",
                        Email = "john@example.com",
                        Sexe = "Homme",
                        DateNais = DateTime.Now.AddYears(-25)
                    },
                   new Etudiant
                   {
                       Nom = "Johnson",
                       Prenom = "Alice",
                       Email = "alice@example.com",
                       Sexe = "Femme",
                       DateNais = DateTime.Now.AddYears(-23)
                   },
                   new Etudiant
                   {
                       Nom = "Smith",
                       Prenom = "Bob",
                       Email = "bob@example.com",
                       Sexe = "Homme",
                       DateNais = DateTime.Now.AddYears(-22)
                   },
                   new Etudiant
                   {
                       Nom = "Williams",
                       Prenom = "Eva",
                       Email = "eva@example.com",
                       Sexe = "Femme",
                       DateNais = DateTime.Now.AddYears(-21)
                   },
                   new Etudiant
                   {
                       Nom = "Brown",
                       Prenom = "Michael",
                       Email = "michael@example.com",
                       Sexe = "Homme",
                       DateNais = DateTime.Now.AddYears(-20)
                   },
                    new Etudiant
                    {
                        Nom = "Gagnon",
                        Prenom = "Sylvie",
                        Email = "sylvie@example.com",
                        Sexe = "Femme",
                        DateNais = DateTime.Now.AddYears(-24)
                    }
               );
                context.SaveChanges();

            }
        }
    }
}
