using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReseñaDTOs;
using AppForSEII2526.API.DTOs.ReseñasDTOs;
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

namespace AppForSEII2526.UT.CrearReseñaTest
{
    public class PostCrearReseña_test : AppForSEII25264SqliteUT
    {
        public PostCrearReseña_test()
        {
            // 1️⃣ Usuarios
            var applicationUser = new List<ApplicationUser>()
{
    new ApplicationUser("1","Mario","Perez","Lopez","Mario") // UserName = "Mario"
};

            // 2️⃣ Tipos de pan
            var tipoPan = new List<TipoPan>()
            {
                new TipoPan(1,"Rustico"),
                new TipoPan(2,"Integral")
            };

            // 3️⃣ Bocadillos
            var bocadillos = new List<Bocadillo>()
            {
                new Bocadillo(1,"Serranito",4.0,10,tipoPan[0],Tamano.Grande),
                new Bocadillo(2,"Completo",3.5,12,tipoPan[1],Tamano.Normal)
            };

            _context.AddRange(applicationUser);
            _context.AddRange(tipoPan);
            _context.AddRange(bocadillos);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesPara_CrearReseña()
        {
            // Sin bocadillos seleccionados
            var dtoSinBocadillos = new ReseñaCreateDTO
            {
                NombreUsuario = "Mario",
                Titulo = "Sugerencia para",
                Descripcion = "Sin bocadillos",
                Valoracion = Resenya.Valoracion_General.Cuatro,
                ReseñaItemDTOs = new List<ReseñaItemDTO>()
            };

            // Bocadillo no existente
            var dtoBocadilloNoExiste = new ReseñaCreateDTO
            {
                NombreUsuario = "Mario",
                Titulo = "Sugerencia para",
                Descripcion = "Bocadillo no existe",
                Valoracion = Resenya.Valoracion_General.Tres,
                ReseñaItemDTOs = new List<ReseñaItemDTO>
                {
                    new ReseñaItemDTO(0,"NoExiste",5.0,Tamano.Normal,7)
                }
            };

            // Usuario inexistente
            var dtoUsuarioNoExiste = new ReseñaCreateDTO
            {
                NombreUsuario = "UsuarioNoExiste",
                Titulo = "Sugerencia para",
                Descripcion = "Usuario inexistente",
                Valoracion = Resenya.Valoracion_General.Tres,
                ReseñaItemDTOs = new List<ReseñaItemDTO>
                {
                    new ReseñaItemDTO(1,"Serranito",4.0,Tamano.Grande,8)
                }
            };

            return new List<object[]>
            {
                new object[] { dtoSinBocadillos, "Bocadillos" },
                new object[] { dtoBocadilloNoExiste, "Bocadillos" },
                new object[] { dtoUsuarioNoExiste, "Usuario" }
            };
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesPara_CrearReseña))]
        public async Task CrearReseña_Error_test(ReseñaCreateDTO dto, string errorKeyEsperado)
        {
            // Arrange
            var mock = new Mock<ILogger<CrearReseñaController>>();
            ILogger<CrearReseñaController> logger = mock.Object;
            var controller = new CrearReseñaController(_context, logger);

            // Act
            var result = await controller.CreateReseña(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
            Assert.True(problemDetails.Errors.ContainsKey(errorKeyEsperado));
        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearReseña_Exito_test()
        {
            // Arrange
            var mock = new Mock<ILogger<CrearReseñaController>>();
            ILogger<CrearReseñaController> logger = mock.Object;
            var controller = new CrearReseñaController(_context, logger);

            var dto = new ReseñaCreateDTO
            {
                NombreUsuario = "Mario",
                Titulo = "Sugerencia para",
                Descripcion = "Sugerencia para",
                Valoracion = Resenya.Valoracion_General.Cinco,
                ReseñaItemDTOs = new List<ReseñaItemDTO>
                {
                    new ReseñaItemDTO(1,"Serranito",4.0,Tamano.Grande,9),
                    new ReseñaItemDTO(2,"Completo",3.5,Tamano.Normal,8)
                }
            };

            // Act
            var result = await controller.CreateReseña(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var reseñaCreada = Assert.IsType<ReseñaDetailDTO>(createdResult.Value);

            Assert.Equal(dto.NombreUsuario, reseñaCreada.NombreUsuario);
            Assert.Equal(dto.Titulo, reseñaCreada.Titulo);
            Assert.Equal(dto.Descripcion, reseñaCreada.Descripcion);
            Assert.Equal(dto.Valoracion, reseñaCreada.Valoracion);
            Assert.Equal(dto.ReseñaItemDTOs.Count, reseñaCreada.ReseñaItemDTOs.Count);

            for (int i = 0; i < dto.ReseñaItemDTOs.Count; i++)
            {
                var esperado = dto.ReseñaItemDTOs[i];
                var actual = reseñaCreada.ReseñaItemDTOs[i];

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
        public async Task CrearReseña_Examen_TEST()
        {
            var mock = new Mock<ILogger<CrearReseñaController>>();
            ILogger<CrearReseñaController> logger = mock.Object;
            var controller = new CrearReseñaController(_context, logger);

            var dto = new ReseñaCreateDTO
            {
                NombreUsuario = "Mario",
                Titulo = "Modificar el bocadillo",
                Descripcion = "Excelente combinación",
                Valoracion = Resenya.Valoracion_General.Cinco,
                ReseñaItemDTOs = new List<ReseñaItemDTO>
                {
                    new ReseñaItemDTO(1,"Serranito",4.0,Tamano.Grande,9),
                    new ReseñaItemDTO(2,"Completo",3.5,Tamano.Normal,8)
                }
            };

            // Act
            var result = await controller.CreateReseña(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error!, el titulo de la reseña debe empezar por sugerencia para", badRequestResult.Value);

        }
    }
}
