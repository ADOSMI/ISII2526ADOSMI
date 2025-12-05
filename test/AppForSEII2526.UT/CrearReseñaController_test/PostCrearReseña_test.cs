using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.ReseñaDTOs;
using AppForSEII2526.API.DTOs.ReseñasDTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
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
            // Datos iniciales
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser("1","Mario","Perez","Lopez","Mario")
            };

            var tipoPan = new List<TipoPan>()
            {
                new TipoPan(1,"Rustico"),
                new TipoPan(2,"Integral")
            };

            var bocadillos = new List<Bocadillo>()
            {
                new Bocadillo(1,"Serranito",4.0,10,tipoPan[0],Tamano.Grande),
                new Bocadillo(2,"Completo",3.5,12,tipoPan[1],Tamano.Normal)
            };

            _context.AddRange(users);
            _context.AddRange(tipoPan);
            _context.AddRange(bocadillos);
            _context.SaveChanges();
        }

        // DATOS DE ERROR SEGÚN LO QUE REALMENTE DEVUELVE EL CONTROLADOR
        public static IEnumerable<object[]> TestCasesPara_CrearReseña()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new ReseñaCreateDTO {
                        NombreUsuario = "Mario",
                        Titulo = "Sugerencia para",
                        Descripcion = "Sin bocadillos",
                        Valoracion = Resenya.Valoracion_General.Cuatro,
                        ReseñaItemDTOs = new List<ReseñaItemDTO>()
                    },
                    "Debe seleccionar al menos un bocadillo para hacer una reseña."
                },

                new object[]
                {
                    new ReseñaCreateDTO {
                        NombreUsuario = "Mario",
                        Titulo = "Sugerencia para",
                        Descripcion = "Bocadillo no existe",
                        Valoracion = Resenya.Valoracion_General.Tres,
                        ReseñaItemDTOs = new List<ReseñaItemDTO> {
                            new ReseñaItemDTO(0,"NoExiste",5,Tamano.Normal,7)
                        }
                    },
                    "Alguno de los bocadillos seleccionados no existe en la base de datos."
                },

                new object[]
                {
                    new ReseñaCreateDTO {
                        NombreUsuario = "UsuarioNoExiste",
                        Titulo = "Sugerencia para",
                        Descripcion = "Usuario inexistente",
                        Valoracion = Resenya.Valoracion_General.Tres,
                        ReseñaItemDTOs = new List<ReseñaItemDTO> {
                            new ReseñaItemDTO(1,"Serranito",4,Tamano.Grande,8)
                        }
                    },
                    "El usuario especificado no existe."
                },

                new object[]
                {
                    new ReseñaCreateDTO {
                        NombreUsuario = "Mario",
                        Titulo = "Modificar título",
                        Descripcion = "Excelente combinación",
                        Valoracion = Resenya.Valoracion_General.Cinco,
                        ReseñaItemDTOs = new List<ReseñaItemDTO> {
                            new ReseñaItemDTO(1,"Serranito",4,Tamano.Grande,9),
                            new ReseñaItemDTO(2,"Completo",3.5,Tamano.Normal,8)
                        }
                    },
                    "Error!, el titulo de la reseña debe empezar por sugerencia para"
                }
            };
        }

        // ⚠ AHORA ESTE TEST COINCIDE CON LOS MENSAJES REALES DEL CONTROLADOR
        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesPara_CrearReseña))]
        public async Task CrearReseña_Error_test(ReseñaCreateDTO dto, string errorEsperado)
        {
            var logger = new Mock<ILogger<CrearReseñaController>>().Object;
            var controller = new CrearReseñaController(_context, logger);

            // Act
            var result = await controller.CreateReseña(dto);

            // Puede ser BadRequest en formato string o ValidationProblemDetails
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);

            if (badRequest.Value is ValidationProblemDetails vpd)
            {
                // Buscar si el mensaje aparece en cualquiera de los ModelState
                string todosLosMensajes =
                    string.Join(" | ", vpd.Errors.SelectMany(e => e.Value));

                Assert.Contains(errorEsperado, todosLosMensajes);
            }
            else
            {
                // Caso del título incorrecto: devuelve string
                var mensajeDevuelto = Assert.IsType<string>(badRequest.Value);
                Assert.Equal(errorEsperado, mensajeDevuelto);
            }
        }

        // ÉXITO
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CrearReseña_Exito_test()
        {
            var logger = new Mock<ILogger<CrearReseñaController>>().Object;
            var controller = new CrearReseñaController(_context, logger);

            var dto = new ReseñaCreateDTO
            {
                NombreUsuario = "Mario",
                Titulo = "Sugerencia para modificar",
                Descripcion = "Sugerencia para",
                Valoracion = Resenya.Valoracion_General.Cinco,
                ReseñaItemDTOs = new List<ReseñaItemDTO>
                {
                    new ReseñaItemDTO(1,"Serranito",4,Tamano.Grande,9),
                    new ReseñaItemDTO(2,"Completo",3.5,Tamano.Normal,8)
                }
            };

            var result = await controller.CreateReseña(dto);
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var reseña = Assert.IsType<ReseñaDetailDTO>(created.Value);

            Assert.Equal(dto.NombreUsuario, reseña.NombreUsuario);
            Assert.Equal(dto.Titulo, reseña.Titulo);
            Assert.Equal(dto.Descripcion, reseña.Descripcion);
            Assert.Equal(dto.Valoracion, reseña.Valoracion);
            Assert.Equal(dto.ReseñaItemDTOs.Count, reseña.ReseñaItemDTOs.Count);
        }
    }
}

