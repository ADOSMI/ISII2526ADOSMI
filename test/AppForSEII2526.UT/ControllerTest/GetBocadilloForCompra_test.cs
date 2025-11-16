using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.BocadilloDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControllerTest
{
    public class GetBocadilloForCompra_test : AppForSEII25264SqliteUT
    {
        public GetBocadilloForCompra_test()
        {
            var tiposPan = new List<TipoPan>()
            {
                new TipoPan(1,"Vegetariano"),
                new TipoPan(2,"Mixto"),
                new TipoPan(3, "Vegano")
            };

            var tamanos = new List<Tamano>()
            {
                Tamano.Grande,
                Tamano.Normal
            };

            var bocadillos = new List<Bocadillo>(){
                new Bocadillo(1,"Bocadillo de jamón", 5.99, 34, tiposPan[0], tamanos[1]),
                new Bocadillo(2,"Bocadillo vegetal", 4.99, 20, tiposPan[2], tamanos[0]),
                new Bocadillo(3,"Bocadillo mixto", 6.49, 15, tiposPan[1], tamanos[0]),
                //bocadillo con stock = 0
                new Bocadillo(4,"Bocadillo de queso", 5.49, 0, tiposPan[0], tamanos[1])
            };


            _context.AddRange(tiposPan);
            _context.AddRange(bocadillos);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> TestCasesPara_GetBocadilloForCompra_OK()
        {
            var bocadilloDTOs = new List<BocadilloForCompraDTO>() {
                new BocadilloForCompraDTO(1,"Bocadillo de jamón",Tamano.Normal,"Vegetariano",5.99),
                new BocadilloForCompraDTO(2,"Bocadillo vegetal",Tamano.Grande,"Vegano",4.99),
                new BocadilloForCompraDTO(3,"Bocadillo mixto",Tamano.Grande,"Mixto",6.49)
            };

            var bocadilloDTOsTC1 = new List<BocadilloForCompraDTO>() { bocadilloDTOs[0], bocadilloDTOs[1], bocadilloDTOs[2] };

            var bocadilloDTOsTC2 = new List<BocadilloForCompraDTO>() { bocadilloDTOs[1], bocadilloDTOs[2] };

            var bocadilloDTOsTC3 = new List<BocadilloForCompraDTO>() { bocadilloDTOs[1] };

            var bocadilloDTOsTC4 = new List<BocadilloForCompraDTO>() { bocadilloDTOs[0] };


            var AllTests = new List<object[]>
            {
                new object[] { null, null, bocadilloDTOsTC1 },
                new object[] { Tamano.Grande, null, bocadilloDTOsTC2 },
                new object[] { null, "Vegano", bocadilloDTOsTC3 },
                new object[] { Tamano.Normal, "Vegetariano", bocadilloDTOsTC4 }
            };

            return (AllTests);


        }

        [Theory]
        [MemberData(nameof(TestCasesPara_GetBocadilloForCompra_OK))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetMoviesForRental_OK_test(Tamano? filtroTamano, string? filtroTipopan,
           IList<BocadilloForCompraDTO> expectedBocadillos)
        {
            // Arrange
            var controller = new BocadilloController(_context, null);

            // Act
            var result = await controller.GetBocadillosForCompra(filtroTamano, filtroTipopan);

            //Assert
            //we check that the response type is OK 
            var okResult = Assert.IsType<OkObjectResult>(result);
            //and obtain the list of movies
            var bocadillosDTOsActual = Assert.IsType<List<BocadilloForCompraDTO>>(okResult.Value);
            Assert.Equal(expectedBocadillos, bocadillosDTOsActual);

        }


    }
}
