using AppForMovies.UT;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReseñaDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AppForSEII2526.UT.CrearReseñaController_test
{
    public class GetCrearReseña_test : AppForSEII25264SqliteUT
    {
        public GetCrearReseña_test()
        {
            // 1️⃣ Tipos de pan
            var tipoPan = new List<TipoPan>()
{
    new TipoPan(1, "Rustico"),
    new TipoPan(2, "Pipa"),
    new TipoPan(3, "Integral"),
    new TipoPan(4, "Baguette"),
    new TipoPan(5, "Avena")
};

            // 2️⃣ Bocadillos
            var bocadillo = new List<Bocadillo>()
{
    new Bocadillo(1,  "Serranito", 4.0, 10, tipoPan[0], Tamano.Grande),
    new Bocadillo(2,  "Serranito", 3.5,  8, tipoPan[1], Tamano.Normal),
    new Bocadillo(3,  "Completo", 3.5, 12, tipoPan[3], Tamano.Normal),
    new Bocadillo(4,  "Politecnico", 4.2, 10, tipoPan[2], Tamano.Grande)
};

            // 3️⃣ Usuarios
            var applicationUser = new List<ApplicationUser>()
{
    new ApplicationUser("1","Mario","Perez","Lopez","MP1"),
    new ApplicationUser("2","Laura","Gomez","Diaz","LG2")
};

            // 4️⃣ Reseñas
            var resenya = new List<Resenya>()
{
    new Resenya(
        "Muy bueno el bocadillo",
        new DateTime(2025,11,16),
        1,
        applicationUser[0],
        "Excelente",
        Resenya.Valoracion_General.Cinco
        )
    {
        NombreUsuario = applicationUser[0].Nombre,  // <-- IMPORTANTE
        ResenyaBocadillos = new List<ResenyaBocadillo>()
        {
            new ResenyaBocadillo(bocadillo[0].Id, 9, 1) { Bocadillo = bocadillo[0] },
            new ResenyaBocadillo(bocadillo[1].Id, 8, 1) { Bocadillo = bocadillo[1] }
        }
    },
    new Resenya(
        "No estaba muy fresco",                         // descripcion
        new DateTime(2025,11,15),                        // fechaPublicacion
        2,                                              // id
        applicationUser[1],                             // applicationUser
        "Regular",                                      // titulo
        Resenya.Valoracion_General.Tres                 // valoracion
    )
    {
        ResenyaBocadillos = new List<ResenyaBocadillo>()
        {
            new ResenyaBocadillo(bocadillo[2].Id, 6, 2) { Bocadillo = bocadillo[2] }
        }
    }
};


            _context.TipoPan.AddRange(tipoPan);
            _context.Bocadillo.AddRange(bocadillo);
            _context.Users.AddRange(applicationUser); // o Users según tu DbContext
            _context.Resenya.AddRange(resenya);
            _context.SaveChanges();



        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCrearResenya_Encontrada_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CrearReseñaController>>();
            ILogger<CrearReseñaController> logger = mock.Object;
            var controller = new CrearReseñaController(_context, logger);

            // Datos esperados basados EXACTAMENTE en lo que insertaste en el constructor
            var itemsEsperados = new List<ReseñaItemDTO>
    {
        new ReseñaItemDTO(
            1,                   // BocadilloId
            "Serranito",         // Nombre
            4.0,                 // PVP
            Tamano.Grande,       // Tamaño
            9                    // Puntuación
        ),
        new ReseñaItemDTO(
            2,
            "Serranito",
            3.5,
            Tamano.Normal,
            8
        )
    };

            var reseñaEsperada = new ReseñaDetailDTO(
                1,                                  // Id
                "Mario",                            // NombreUsuario (viene del ApplicationUser)
                "Excelente",                        // Titulo
                "Muy bueno el bocadillo",           // Descripción
                new DateTime(2025, 11, 16),         // FechaPublicacion
                Resenya.Valoracion_General.Cinco,   // Valoración
                itemsEsperados                      // Lista bocadillos
            );

            // Act
            var result = await controller.GetCrearReseña(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reseñaActual = Assert.IsType<ReseñaDetailDTO>(okResult.Value);

            Assert.Equal(reseñaEsperada.Id, reseñaActual.Id);
            Assert.Equal(reseñaEsperada.NombreUsuario, reseñaActual.NombreUsuario);
            Assert.Equal(reseñaEsperada.Titulo, reseñaActual.Titulo);
            Assert.Equal(reseñaEsperada.Descripcion, reseñaActual.Descripcion);
            Assert.Equal(reseñaEsperada.Valoracion, reseñaActual.Valoracion);

            // Comparar fecha solo por día, ignorando la hora
            Assert.Equal(reseñaEsperada.FechaPublicacion.Date, reseñaActual.FechaPublicacion.Date);

            // Comparar lista de items
            Assert.Equal(reseñaEsperada.ReseñaItemDTOs.Count, reseñaActual.ReseñaItemDTOs.Count);
            for (int i = 0; i < reseñaEsperada.ReseñaItemDTOs.Count; i++)
            {
                var esperado = reseñaEsperada.ReseñaItemDTOs[i];
                var actual = reseñaActual.ReseñaItemDTOs[i];

                Assert.Equal(esperado.Id, actual.Id);
                Assert.Equal(esperado.Nombre, actual.Nombre);
                Assert.Equal(esperado.PVP, actual.PVP);
                Assert.Equal(esperado.Tamano, actual.Tamano);
                Assert.Equal(esperado.Puntuacion, actual.Puntuacion);
            }
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetCrearResenya_NoEncontrada_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CrearReseñaController>>();
            ILogger<CrearReseñaController> logger = mock.Object;

            var controller = new CrearReseñaController(_context, logger);

            // Act
            var result = await controller.GetCrearReseña(999); // ID inexistente

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



    }
}