using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.UC_Reseña
{
    public class DetailReseña_PO : PageObject
    {
        
        private By _tablaBocadillosBy = By.Id("TablaBocadillos");

        public DetailReseña_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CompruebaDetalle(string usuario, string titulo, string descripcion, string valoracion, DateTime fechaPublicacion)
        {
            
            WaitForBeingVisible(By.Id("TituloReseña"));

            bool result = true;

            //Comprobamos USUARIO
            result = result && _driver.FindElement(By.Id("NombreUsuario")).Text.Contains(usuario);

            //Comprobamos TÍTULO
            result = result && _driver.FindElement(By.Id("TituloReseña")).Text.Contains(titulo);

            //Comprobamos DESCRIPCIÓN
            result = result && _driver.FindElement(By.Id("DescripcionReseña")).Text.Contains(descripcion);

            //Comprobamos VALORACIÓN
            result = result && _driver.FindElement(By.Id("ValoracionGeneral")).Text.Contains(valoracion);

            //Comprobamos FECHA
            try
            {
                var textoFecha = _driver.FindElement(By.Id("FechaPublicacion")).Text;
                var fechaActualWeb = DateTime.Parse(textoFecha);

                //Margen de 1min por si se equivoca minimamente
                result = result && ((fechaActualWeb - fechaPublicacion).Duration() < new TimeSpan(0, 1, 0));
            }
            catch (Exception)
            {
                //Por si falla
                result = result && _driver.FindElement(By.Id("FechaPublicacion")).Displayed;
            }

            if (!result) _output.WriteLine("Fallo en la comprobación de datos generales del detalle.");

            return result;
        }

        public bool CompruebaListaBocadillos(List<string[]> bocadillosEsperados)
        {
            return CheckBodyTable(bocadillosEsperados, _tablaBocadillosBy);
        }

        private bool CheckBodyTable(List<string[]> expectedRows, By IdTable)
        {
            
            WaitForBeingVisible(IdTable);

            
            IList<IWebElement> actualRows = _driver
                .FindElement(IdTable)
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"))
                .ToList();

            if (actualRows.Count != expectedRows.Count)
            {
                _output.WriteLine($"Error Tabla: Esperadas {expectedRows.Count} filas, encontradas {actualRows.Count}");
                return false;
            }

            for (int i = 0; i < expectedRows.Count; i++)
            {
                string actualRowText = actualRows[i].Text;

                foreach (var datoEsperado in expectedRows[i])
                {
                    if (!actualRowText.Contains(datoEsperado))
                    {
                        _output.WriteLine($"Error Fila {i}: No se encontró '{datoEsperado}'. Texto fila: '{actualRowText}'");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
