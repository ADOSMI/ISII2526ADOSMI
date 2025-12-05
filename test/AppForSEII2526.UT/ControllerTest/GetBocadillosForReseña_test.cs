using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs.BocadilloDTOs;
using AppForSEII2526.API.Data;
using AppForSEII2526.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControllerTest
{
    public class GetBocadilllosForReseña_test : AppForSEII25264SqliteUT
    {
        public GetBocadilllosForReseña_test()
        {
            var tipoPan = new List<TipoPan>()
            {
                new TipoPan(1, "Rustico"),
                new TipoPan(2, "Pipa"),
                new TipoPan(3, "Integral"),
                new TipoPan(4, "Baguette"),
                new TipoPan(5, "Avena")
            };

            var bocadillo = new List<Bocadillo>()
            {
                new Bocadillo(1,  "Serranito", 4.0, 10, tipoPan[0], Tamano.Grande),
                new Bocadillo(2,  "Serranito", 3.5,  8, tipoPan[1], Tamano.Normal),
                new Bocadillo(3,  "Completo", 3.5, 12, tipoPan[3], Tamano.Normal),
                new Bocadillo(4,  "Politecnico", 4.2, 10, tipoPan[2], Tamano.Grande),
            };

            _context.AddRange(bocadillo);
            _context.AddRange(tipoPan);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesPara_GetBocadillosForReseña()
        {
            var bocadilloDTOs = new List<BocadilloForReseñaDTO>()
            {
                new BocadilloForReseñaDTO(1, "Serranito", 4.0, Tamano.Grande, "Rustico"),
                new BocadilloForReseñaDTO(2, "Serranito", 3.5, Tamano.Normal, "Pipa"),
                new BocadilloForReseñaDTO(3, "Completo", 3.5, Tamano.Normal, "Baguette"),
                new BocadilloForReseñaDTO(4, "Politecnico", 4.2, Tamano.Grande, "Integral"),
            };

            // Caso 1: sin filtros → todos
            var bocadilloDTOsTC1 = new List<BocadilloForReseñaDTO>()
            {
                bocadilloDTOs[0], bocadilloDTOs[1], bocadilloDTOs[2], bocadilloDTOs[3]
            };

            // Caso 2: solo por pvp = 3.5 → ninguno (PVP < 3.5)
            var bocadilloDTOsTC2 = new List<BocadilloForReseñaDTO>();

            // Caso 3: solo por nombre = "Serranito" → id 1 y 2
            var bocadilloDTOsTC3 = new List<BocadilloForReseñaDTO>()
            {
                bocadilloDTOs[0], bocadilloDTOs[1]
            };

            // Caso 4: nombre "Serranito" + pvp = 3.5 → ninguno (PVP < 3.5)
            var bocadilloDTOsTC4 = new List<BocadilloForReseñaDTO>();

            return new List<object[]>
            {
                new object[] { bocadilloDTOsTC1, null, null },          // sin filtros
                new object[] { bocadilloDTOsTC2, null, 3.5 },          // filtro PVP < 3.5
                new object[] { bocadilloDTOsTC3, "Serranito", null },   // filtro nombre
                new object[] { bocadilloDTOsTC4, "Serranito", 3.5 },    // nombre + pvp (ninguno)
            };
        }

        [Theory]
        [MemberData(nameof(TestCasesPara_GetBocadillosForReseña))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetBocadillosForReseñaTest(
            IList<BocadilloForReseñaDTO> bocadillosEsperados,
            string? nombre,
            double? pvp)
        {
            // Arrange
            var controller = new BocadilloController(_context, null);

            // Act
            var result = await controller.GetBocadillosForReseña(nombre, pvp);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var bocadilloDTOsActual = Assert.IsType<List<BocadilloForReseñaDTO>>(okResult.Value);

            Assert.Equal(bocadillosEsperados, bocadilloDTOsActual);
        }
    }
}


