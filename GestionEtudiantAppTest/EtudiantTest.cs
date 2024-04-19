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

            // Initialisez le mod�le de l'�tudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "John",
                Prenom = "Doe",
                Email = "john@example.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-25)
            };

            // Act
            var result = await controller.Create(etudiant) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // V�rifiez si l'�tudiant a �t� ajout� � la base de donn�es
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync();
            Assert.NotNull(etudiantInDatabase);
            Assert.Equal("John", etudiantInDatabase.Nom);
            Assert.Equal("Doe", etudiantInDatabase.Prenom);
        }

        [Fact]
        public async Task DeleteEtudiantTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GestionEtudiantAppDbContext>()
                .UseInMemoryDatabase(databaseName: "SeleniumAppDB")
                .Options;

            using var context = new GestionEtudiantAppDbContext(options);
            var controller = new EtudiantsController(context);

            // Initialisez le mod�le de l'�tudiant avec des valeurs valides
            var etudiant = new Etudiant
            {
                Nom = "John",
                Prenom = "Doe",
                Email = "john@example.com",
                Sexe = "Homme",
                DateNais = DateTime.Now.AddYears(-25)
            };

            // Ajoutez l'�tudiant � la base de donn�es
            await controller.Create(etudiant);

            // R�cup�rez l'ID de l'�tudiant ajout�
            int etudiantId = etudiant.Id;

            // Act
            var result = await controller.Delete(etudiantId) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // V�rifiez si l'�tudiant a �t� correctement r�cup�r� pour la suppression
            var etudiantToDelete = result.Model as Etudiant;
            Assert.NotNull(etudiantToDelete);

            // Supprimez l'�tudiant
            var deleteResult = await controller.DeleteConfirmed(etudiantId) as RedirectToActionResult;
            Assert.NotNull(deleteResult);
            Assert.Equal("Index", deleteResult.ActionName);

            // V�rifiez que l'�tudiant a �t� supprim� de la base de donn�es
            var etudiantInDatabase = await context.Etudiants.FirstOrDefaultAsync(e => e.Id == etudiantId);
            Assert.Null(etudiantInDatabase);
        }


    }


}

