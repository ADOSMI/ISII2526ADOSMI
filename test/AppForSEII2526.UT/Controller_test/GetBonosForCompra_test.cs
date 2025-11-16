using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.BonosDTOs;
using AppForSEII2526.API.Models;
using AppForSEII2526.UT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace AppForSEII2526.UT.Controllers_test
{
    public class GetBonosForCompra_test : AppForSEII25264SqliteUT
    {
        public GetBonosForCompra_test() 
        {
            var tipoBocadillo = new List<TipoBocadillo>()
            {
                new TipoBocadillo(1,"Completo"),
                new TipoBocadillo(2,"Serrano"),
                new TipoBocadillo(3,"Politecnico")
            };

            var bonos = new List<BonoBocadillo>()
            {
                new BonoBocadillo(tipoBocadillo[0],1,5,2,"Bono1",2,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[1],2,5,3,"Bono2",3,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[1],3,5,3,"Bono3",3,new List<BonosComprados>()),
                new BonoBocadillo(tipoBocadillo[2],4,5,4,"Bono3",4,new List<BonosComprados>()),
            };

            


            _context.AddRange(tipoBocadillo);
            _context.AddRange(bonos);
            _context.SaveChanges();

            
        }


        public static IEnumerable<object[]> TestCasesPara_GetBonosForCompra()
        {

            var bonosDTOs = new List<BonosForCompraDTO>() {

                new BonosForCompraDTO("Bono1",2,2,"Completo"),
                new BonosForCompraDTO("Bono2",3,3,"Serrano"),
                new BonosForCompraDTO("Bono3",3,3,"Serrano"),
                new BonosForCompraDTO("Bono3",4,4,"Politecnico")
            };

            var bonosDTOsTC1 = new List<BonosForCompraDTO>() { bonosDTOs[0], bonosDTOs[1], bonosDTOs[2], bonosDTOs[3], };

            var bonosDTOsTC2 = new List<BonosForCompraDTO>() { bonosDTOs[0] };

            var bonosDTOsTC3 = new List<BonosForCompraDTO>() { bonosDTOs[1], bonosDTOs[2] };

            var bonosDTOsTC4 = new List<BonosForCompraDTO>() { bonosDTOs[2], bonosDTOs[3] };

            var bonosDTOsTC5 = new List<BonosForCompraDTO>() { bonosDTOs[3] };



            var allTests = new List<object[]>
            {             //Filtros aplicados y bonos esperados
                new object[] { null, null, bonosDTOsTC1, }, //si no hay filtros tienen que aparecer todos los bonos
                new object[] { null, "Completo", bonosDTOsTC2, }, //filtramos por aquellos bonos con tipoBocadillo "Completo"
                new object[] { null, "Serrano", bonosDTOsTC3, }, //filtramos por aquellos bonos con tipoBocadillo "Serrano"                                                            
                new object[] { "Bono3", null, bonosDTOsTC4, }, //nombre bono "Bono3"
                new object[] { "Bono3", "Politecnico", bonosDTOsTC5,  }, //nombre bono "Bono3" y tipoBocadillo "Politecnico"
            };

            return allTests;
        }

        [Theory]
        [MemberData(nameof(TestCasesPara_GetBonosForCompra))]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBonosCompra_test(string? filtroTipoBocadillo, string? filtroNombreBono, IList<BonosForCompraDTO> bonosEsperados)
        {
            // Arrange
            var controller = new BonosController(_context, null);

            // Act
            var result = await controller.GetBonosForCompra(filtroTipoBocadillo, filtroNombreBono);

            //Assert
            //Comprobamos que la respuesta es de tipo Ok y obtenemos la lista de videojuegos
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bonoDTOsActual = Assert.IsType<List<BonosForCompraDTO>>(okResult.Value);

            //Comprobamos que el esperado y el actual son iguales
            Assert.Equal(bonosEsperados, bonoDTOsActual);

        }





    }
}