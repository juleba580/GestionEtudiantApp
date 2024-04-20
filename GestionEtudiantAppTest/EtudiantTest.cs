using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GestionEtudiantApp.Controllers;
using GestionEtudiantApp.Models;
using Xunit;
using System.Threading.Tasks;

namespace GestionEtudiantAppTest
{
    public class EtudiantTest
    {
        [Fact]
        public async Task CreateEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GestionEtudiantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "GestionEtudiantAppDB")
                .Options;

            using var context = new GestionEtudiantAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "bsm",
                Prenom = "Jule",
                Email = "julo@gmail.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-13)
            };

            // Act
            var result = await controller.Create(etudiant) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Vérifiez si l'étudiant a été ajouté à la base de données
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync();
            Assert.NotNull(etudiantInDatabase);
            Assert.Equal("bsm", etudiantInDatabase.Nom);
            Assert.Equal("Jule", etudiantInDatabase.Prenom);
        }

        [Fact]
        public async Task DeleteEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GestionEtudiantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "GestionEtudiantAppDB")
                .Options;

            using var context = new GestionEtudiantAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "bsm",
                Prenom = "Jule",
                Email = "julo@gmail.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-13)
            };

            // Ajoutez l'étudiant à la base de données
            await controller.Create(etudiant);

            // Récupérez l'ID de l'étudiant ajouté
            int etudiantId = etudiant.Id;

            // Act
            var result = await controller.Delete(etudiantId) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Vérifiez si l'étudiant a été correctement récupéré pour la suppression
            var etudiantToDelete = result.Model as Etudiant;
            Assert.NotNull(etudiantToDelete);

            // Supprimez l'étudiant
            var deleteResult = await controller.DeleteConfirmed(etudiantId) as RedirectToActionResult;
            Assert.NotNull(deleteResult);
            Assert.Equal("Index", deleteResult.ActionName);

            // Vérifiez que l'étudiant a été supprimé de la base de données
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync(e => e.Id == etudiantId);
            Assert.Null(etudiantInDatabase);
        }

        [Fact]
        public async Task EditEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GestionEtudiantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "GestionEtudiantAppDB")
                .Options;

            using var context = new GestionEtudiantAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "bsm",
                Prenom = "Jule",
                Email = "julo@gmail.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-13)
            };

            // Ajoutez l'étudiant à la base de données
            await controller.Create(etudiant);

            // Récupérez l'ID de l'étudiant ajouté
            int etudiantId = etudiant.Id;

            // Mettez à jour les informations de l'étudiant
            etudiant.Prenom = "Julien";

            // Act
            var result = await controller.Edit(etudiantId, etudiant) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Vérifiez si les informations de l'étudiant ont été correctement mises à jour dans la base de données
            var etudiantInDatabase = await context.Etudiants.FindAsync(etudiantId);
            Assert.NotNull(etudiantInDatabase);
            Assert.Equal("Julien", etudiantInDatabase.Prenom);
        }

        [Fact]
        public async Task DetailsEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GestionEtudiantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "GestionEtudiantAppDB")
                .Options;

            using var context = new GestionEtudiantAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le modèle de l'étudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "bsm",
                Prenom = "Jule",
                Email = "julo@gmail.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-13)
            };

            // Ajoutez l'étudiant à la base de données
            await controller.Create(etudiant);

            // Récupérez l'ID de l'étudiant ajouté
            int etudiantId = etudiant.Id;

            // Act
            var result = await controller.Details(etudiantId) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Vérifiez si les informations de l'étudiant sont correctement affichées dans la vue des détails
            var etudiantDetails = result.Model as Etudiant;
            Assert.NotNull(etudiantDetails);
            Assert.Equal("bsm", etudiantDetails.Nom);
            Assert.Equal("Jule", etudiantDetails.Prenom);
            // Ajoutez des assertions supplémentaires pour les autres propriétés de l'étudiant si nécessaire
        }
    }
}
