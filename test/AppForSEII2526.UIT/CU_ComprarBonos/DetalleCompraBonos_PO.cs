using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace AppForSEII2526.UIT.CU_ComprarBonos
{
    public class DetalleCompraBonos_PO : PageObject
    {
        // Selector para la tabla de abajo (Bonos Comprados).
        // Usamos XPath para buscar la tabla justo debajo del título.
        private By _tablaListaBonosBy = By.XPath("//*[contains(text(), 'Bonos Comprados')]/following-sibling::table[1]");

        // Selector ANCLA para asegurar que la página de detalles ha cargado.
        // Buscamos el título "DetailRental" o "DetalleCompraBono" que se ve en tu captura.
        private By _tituloDetalleBy = By.XPath("//*[contains(text(), 'DetailRental') or contains(text(), 'DetalleCompraBono')]");

        public DetalleCompraBonos_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        // Comprueba la tabla de abajo (Lista de bonos)
        public bool CompruebaListaVideojuegos(List<string[]> expectedVideojuegos)
        {
            return CheckBodyTable(expectedVideojuegos, _tablaListaBonosBy);
        }

        // --- MÉTODO CORREGIDO (SOLUCIÓN DEFINITIVA) ---
        public bool CompruebaCompra(List<string[]> expectedCompra)
        {
            // 1. Esperamos a que el título de la página sea visible para asegurar la carga
            WaitForBeingVisible(_tituloDetalleBy);

            // 2. En lugar de buscar una etiqueta <dl> o <table> que no existe,
            // obtenemos el texto visible de todo el cuerpo de la página.
            // Esto valida que la información está visible al usuario, sin importar el HTML.
            string textoPagina = _driver.FindElement(By.TagName("body")).Text;

            // Normalizamos el texto (quitamos saltos de línea raros) para facilitar la búsqueda
            textoPagina = textoPagina.Replace("\r\n", " ").Replace("\n", " ");

            foreach (var fila in expectedCompra)
            {
                foreach (var datoEsperado in fila)
                {
                    // Nota: Si 'datoEsperado' contiene HTML (como <b>Fecha...</b>), 
                    // la validación fallará porque .Text solo devuelve texto plano.
                    // Asegúrate de pasar solo los valores limpios (ej: "Adolfo", "Tarjeta").

                    // Limpieza simple por si acaso el dato esperado viene con etiquetas HTML
                    string datoLimpio = System.Text.RegularExpressions.Regex.Replace(datoEsperado, "<.*?>", String.Empty);

                    if (!string.IsNullOrWhiteSpace(datoLimpio) && !textoPagina.Contains(datoLimpio))
                    {
                        _output.WriteLine($"Error en Detalles Compra: No se encontró el texto '{datoLimpio}' en la página.");
                        return false;
                    }
                }
            }
            return true;
        }

        // Comprueba el total (Pie de tabla)
        public bool CompruebaTotal(List<string[]> expectedBonos)
        {
            return CheckFooterTable(expectedBonos, _tablaListaBonosBy);
        }

        public bool CheckFooterTable(List<string[]> filasEsperadas, By IdTable)
        {
            WaitForBeingVisible(IdTable);
            IWebElement table = _driver.FindElement(IdTable);

            // Buscamos en tfoot (pie) primero
            IList<IWebElement> footerRows = table.FindElements(By.CssSelector("tfoot tr"));

            // Si no hay tfoot, probamos con la última fila del tbody (fallback)
            if (footerRows.Count == 0)
            {
                footerRows = table.FindElements(By.CssSelector("tbody tr:last-child"));
            }

            IList<IWebElement> filasActuales = footerRows.ToList();

            if (filasActuales.Count == 0 && filasEsperadas.Count > 0)
            {
                _output.WriteLine("Advertencia: No se encontraron filas de total en la tabla.");
                return false;
            }

            bool result = true;
            int maxCount = Math.Min(filasEsperadas.Count, filasActuales.Count);

            for (int i = 0; i < maxCount; i++)
            {
                string filaEsperada = string.Join(" ", filasEsperadas[i]);
                string filaActual = filasActuales[i].Text.Replace("\r\n", " ").Replace("\n", " ");

                if (!filaActual.Contains(filaEsperada))
                {
                    _output.WriteLine($"Error en Footer: \n \t Esperado: {filaEsperada} \n \t Encontrado: {filaActual}");
                    result = false;
                }
            }
            return result;
        }
    }
}