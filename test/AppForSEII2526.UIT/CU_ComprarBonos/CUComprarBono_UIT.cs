using AppForMovies.UIT.Shared;
using AppForSEII2526.UIT.CU_ComprarBonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarBonos
{
    public class CUComprarBono_UIT : UC_UIT
    {
        private SeleccionarBonos_PO SeleccionarBonos_PO;

        public CUComprarBono_UIT(ITestOutputHelper output) : base(output)
        {
            SeleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);
        }

       

        /*

        [Fact]
        public void CU1_1_Flujo_Basico()
        {
            // Arrange
            var seleccionarVideojuegos_PO = new SeleccionarBonos_PO(_driver, _output);
            var crearCompra_PO = new CrearCompra_PO(_driver, _output);
            var detalleCompra_PO = new DetalleCompra_PO(_driver, _output);

            string nombreApellidosCliente = "Carlos Rubio";
            string email = "carlos@gmail.com";
            string direccion = "Calle 1";
            string personaEncargada = "Carlos";
            string metodoPago = "TarjetaCredito";
            string sugerenciaEntrega = "a las 12h";
            string edad = "16";

            string nombreUsuario = "carlos@gmail.com";
            string precioTotal = "69,99";


            string cabeceraEsperada = "Resumen de la Compra";
            string fechaEsperada = "<b>Fecha de compra: </b> " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm").ToUpper();


            var videojuegosEsperados = new List<string[]>();
            videojuegosEsperados.Add(new string[] { "The last of us", "9", "18", "PC", "69,99" });

            var expectedCompra = new List<string[]> { new string[] { nombreApellidosCliente, nombreUsuario, edad, direccion, precioTotal } };



            // Act ---------
            Inicio();
            // Navegar hasta la página de Comprar Videojuegos.
            Ir_A_ComprarVideojuegos();
            // Seleccionar el videojuego 1
            seleccionarVideojuegos_PO.SeleccionarVideojuegos(new List<string>() { "1" });
            // Pulsar el botón de comprar.
            seleccionarVideojuegos_PO.Comprar();
            // Seleccionar como método de pago Tarjeta.
            crearCompra_PO.setMetodoPago("TarjetaCredito");
            // Poner las cantidades.
            crearCompra_PO.setCantidad("1", "1");
            //Datos de la compra
            crearCompra_PO.setDatos(nombreApellidosCliente, email, direccion, personaEncargada, sugerenciaEntrega, edad, metodoPago);

            // Pulsar comprar para finalizar la compra.
            crearCompra_PO.Comprar();
            // Ahora se debe haber mostrado la página de detalle y puedo comprobar si todo ha ido bien.

            Console.Write(_driver.PageSource);
            System.Threading.Thread.Sleep(2000);
            // Assert
            Assert.True(_driver.PageSource.Contains(cabeceraEsperada));
            Assert.True(_driver.PageSource.Contains(fechaEsperada));
            Assert.True(detalleCompra_PO.CompruebaTotal(videojuegosEsperados));
            Assert.True(detalleCompra_PO.CompruebaCompra(expectedCompra)); //Comprobamos los detalles de la compra
            Assert.True(detalleCompra_PO.CompruebaListaVideojuegos(videojuegosEsperados)); //Comprobamos la lista de videojuegos comprados


        }

        */

        [Fact]
        public void UC3_1_No_Hay_Bonos()
        {
            // Arrange -----------
            var textoEsperado = "No se ha encontrado ningun bono disponible.";
            var seleccionarVideojuegos_PO = new SeleccionarBonos_PO(_driver, _output);

            // Act ---------
            Initial_step_opening_the_web_page();

            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();

            // Filtrar los artículos.
            seleccionarVideojuegos_PO.FiltrarBonosForCompra("Paco", "");

            System.Threading.Thread.Sleep(500);

            // Assert ----------
            // Comprobar que la lista de artículos que ha devuelto es la correcta.
            Assert.Contains(textoEsperado, "No se ha encontrado ningun bono disponible.");
        }

        // Está es la prueba de ejemplo del filtrado por edad de videojuego sin usar Theory.
        [Fact]
        public void UC3_2_FiltradoPorNombreBono()
        {
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroNombre = "Adolfo";
            string filtroTipoBocadillo = "";
            

            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Adolfo";
            string precio = "3";
            string numeroBocadillos = "3";
            string tipoBocadillo = "Serrano";
            

            var seleccionarVideojuegos_PO = new SeleccionarBonos_PO(_driver, _output);
            var bonosEsperados = new List<string[]> { new string[] { nombre, precio, numeroBocadillos, tipoBocadillo } };

            // Act ---------
            Initial_step_opening_the_web_page();

            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();
            
            // Filtrar los videojuegos.
            seleccionarVideojuegos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);

            // Assert ----------
            // Comprobar que la lista de videojuegos que ha devuelto es la correcta.
            Assert.True(seleccionarVideojuegos_PO.CompruebaListaBonos(bonosEsperados));
        }

        [Fact]
        public void UC3_3_FiltradoPorTipoBocadillo()
        {
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroNombre = "";
            string filtroTipoBocadillo = "Bufalo";


            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Bono1";
            string precio = "4";
            string numeroBocadillos = "5";
            string tipoBocadillo = "Bufalo";


            var seleccionarVideojuegos_PO = new SeleccionarBonos_PO(_driver, _output);
            var bonosEsperados = new List<string[]> { new string[] { nombre, precio, numeroBocadillos, tipoBocadillo } };

            // Act ---------
            Initial_step_opening_the_web_page();

            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();

            // Filtrar los videojuegos.
            seleccionarVideojuegos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);

            // Assert ----------
            // Comprobar que la lista de videojuegos que ha devuelto es la correcta.
            Assert.True(seleccionarVideojuegos_PO.CompruebaListaBonos(bonosEsperados));
        }

        [Fact]
        public void UC3_4_Carrito_Vacio()
        {
            // Arrange
            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);

            // Act ---------
            Initial_step_opening_the_web_page();

            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();

            // Assert
            Assert.False(seleccionarBonos_PO.isEnabledComprar());
        }

        [Fact]
        public void UC3_5_ModificarCarrito()
        {
            // Arrange -----------
            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);

            // Usamos los nombres reales
            var listaNombres = new List<string> { "Adolfo", "Bono1" };

            // Precios esperados (Adolfo=3, Bono1=4)
            string precioTotalInicial = "7"; // 3 + 4
            string precioTotalFinal = "3";   // Solo Adolfo


            // Act ---------
            Initial_step_opening_the_web_page();

            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();

            // Filtrar los videojuegos.
            seleccionarBonos_PO.SeleccionarBonos(listaNombres);

            // Seleccionamos usando los nombres
            seleccionarBonos_PO.Comprar();

            // 3. Verificamos que el precio total sea 7 (Antes de modificar)
            // NOTA: Asegúrate de que en CrearCompraBono.razor el precio tenga id="precioTotal"
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("precioTotal"));
            var precioElemento = _driver.FindElement(By.Id("precioTotal"));

            // Comprobamos que el texto contenga "7"
            Assert.Contains(precioTotalInicial, precioElemento.Text);

            // 4. Pulsamos el botón de Modificar/Volver para regresar a la selección
            // NOTA: Asegúrate de que tu botón de volver tenga id="botonVolver" o "botonModificar"
            _driver.FindElement(By.Id("botonVolver")).Click();

            // Deseleccionamos "Bono1" (esto pulsará la X en el carrito)
            seleccionarBonos_PO.DeseleccionarBono("Bono1");

            // 6. Volvemos a darle a Comprar
            seleccionarBonos_PO.Comprar();

            // Assert ----------
            // 7. Verificamos que el precio se ha actualizado (Ahora debe ser 3)
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("precioTotal"));
            var precioFinalElemento = _driver.FindElement(By.Id("precioTotal"));

            Assert.Contains(precioTotalFinal, precioFinalElemento.Text);
        }

        [Fact]
        public void UC3_6_FaltanDatosObligatorios_Nombre()
        {
            // Arrange
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroNombre = "";
            string filtroTipoBocadillo = "";


            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Bono1";
            string precio = "4";
            string numeroBocadillos = "5";
            string tipoBocadillo = "Bufalo";
            string metodoPago = "Tarjeta";

            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);
            var crearCompraBonos_PO = new CrearCompraBonos_PO(_driver, _output);
            var listaNombres = new List<string> { "Adolfo"};

            


            // Act ---------
            Initial_step_opening_the_web_page();
            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();
            //Aplicar el filtro
            seleccionarBonos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);
            //Adquirir el videojuego
            seleccionarBonos_PO.SeleccionarBonos(listaNombres);
            // Pulsar el botón de comprar.
            seleccionarBonos_PO.Comprar();
            // Seleccionar como método de pago Tarjeta.
            crearCompraBonos_PO.setMetodoPago("Tarjeta");
            crearCompraBonos_PO.setDatos("", "Escribano", "Martinez", metodoPago);


            //Pulsamos en comprar
            crearCompraBonos_PO.Comprar();

            System.Threading.Thread.Sleep(2000);

            // Assert
            Assert.True(_driver.PageSource.Contains("The Nombre field is required."));
        }

        [Fact]
        public void UC3_7_FaltanDatosObligatorios_PrimerApellido()
        {
            // Arrange
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroNombre = "";
            string filtroTipoBocadillo = "";


            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Bono1";
            string precio = "4";
            string numeroBocadillos = "5";
            string tipoBocadillo = "Bufalo";
            string metodoPago = "Tarjeta";

            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);
            var crearCompraBonos_PO = new CrearCompraBonos_PO(_driver, _output);
            var listaNombres = new List<string> { "Adolfo" };




            // Act ---------
            Initial_step_opening_the_web_page();
            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();
            //Aplicar el filtro
            seleccionarBonos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);
            //Adquirir el videojuego
            seleccionarBonos_PO.SeleccionarBonos(listaNombres);
            // Pulsar el botón de comprar.
            seleccionarBonos_PO.Comprar();
            // Seleccionar como método de pago Tarjeta.
            crearCompraBonos_PO.setMetodoPago("Tarjeta");
            crearCompraBonos_PO.setDatos("Adolfo", "", "Martinez", metodoPago);


            //Pulsamos en comprar
            crearCompraBonos_PO.Comprar();

            System.Threading.Thread.Sleep(2000);

            // Assert
            Assert.True(_driver.PageSource.Contains("The Apellido1 field is required."));
        }

        [Fact]
        public void UC3_7_FaltanDatosObligatorios_SegundoApellido()
        {
            // Arrange
            // Arrange -----------
            // Se definen los valores de los filtros.
            string filtroNombre = "";
            string filtroTipoBocadillo = "";


            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Bono1";
            string precio = "4";
            string numeroBocadillos = "5";
            string tipoBocadillo = "Bufalo";
            string metodoPago = "Tarjeta";

            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);
            var crearCompraBonos_PO = new CrearCompraBonos_PO(_driver, _output);
            var listaNombres = new List<string> { "Adolfo" };




            // Act ---------
            Initial_step_opening_the_web_page();
            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();
            //Aplicar el filtro
            seleccionarBonos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);
            //Adquirir el videojuego
            seleccionarBonos_PO.SeleccionarBonos(listaNombres);
            // Pulsar el botón de comprar.
            seleccionarBonos_PO.Comprar();
            // Seleccionar como método de pago Tarjeta.
            crearCompraBonos_PO.setMetodoPago("Tarjeta");
            crearCompraBonos_PO.setDatos("Adolfo", "Escribano", "", metodoPago);


            //Pulsamos en comprar
            crearCompraBonos_PO.Comprar();

            System.Threading.Thread.Sleep(2000);

            // Assert
            Assert.True(_driver.PageSource.Contains("The Apellido2 field is required."));
        }

        [Fact]
        public void UC3_8_ModificarCarrito_GuardandoDatos()
        {

            var seleccionarBonos_PO = new SeleccionarBonos_PO(_driver, _output);
            var crearCompraBonos_PO = new CrearCompraBonos_PO(_driver, _output);

            // Usamos los nombres reales
            var listaNombres = new List<string> { "Adolfo", "Bono1" };

            
            string filtroNombre = "";
            string filtroTipoBocadillo = "";


            // En este caso (con los datos que hay cargados) sólo debe devolver la fila con los siguientes datos.
            string nombre = "Bono1";
            string precio = "4";
            string numeroBocadillos = "5";
            string tipoBocadillo = "Bufalo";

            string nombreInput = "Adolfo";
            string ap1Input = "Escribano";
            string ap2Input = "Martinez";
            string metodoPago = "Tarjeta";


            // Act ---------
            Initial_step_opening_the_web_page();
            
            // Navegar hasta la página de Comprar Videojuegos.
            SeleccionarBonos_PO.WaitForBeingVisible(By.Id("nav-comprar-bono"));
            _driver.FindElement(By.Id("nav-comprar-bono")).Click();
            
            //Aplicar el filtro
            seleccionarBonos_PO.FiltrarBonosForCompra(filtroNombre, filtroTipoBocadillo);
            
            //Adquirir el videojuego
            seleccionarBonos_PO.SeleccionarBonos(listaNombres);
            
            // Pulsar el botón de comprar.
            seleccionarBonos_PO.Comprar();
            
            // Seleccionar como método de pago Tarjeta.
            crearCompraBonos_PO.setMetodoPago(metodoPago);
            crearCompraBonos_PO.setDatos(nombreInput, ap1Input, ap2Input, metodoPago);

            // Pulsamos el botón de Modificar/Volver para regresar a la selección
            crearCompraBonos_PO.Volver();

            // Deseleccionamos "Bono1" (esto pulsará la X en el carrito)
            seleccionarBonos_PO.DeseleccionarBono("Bono1");

            // Volvemos a darle a Comprar
            seleccionarBonos_PO.Comprar();

            string nombreActual = _driver.FindElement(By.Id("Nombre")).GetAttribute("value");
            string ap1Actual = _driver.FindElement(By.Id("Primer Apellido")).GetAttribute("value");
            string ap2Actual = _driver.FindElement(By.Id("Segundo Apellido")).GetAttribute("value");
            string metodoActual = _driver.FindElement(By.Id("MetodoPago")).GetAttribute("value");

            // Assert

            Assert.Equal(nombreInput, nombreActual);
            Assert.Equal(ap1Input, ap1Actual);
            Assert.Equal(ap2Input, ap2Actual);
            Assert.Equal(metodoPago, metodoActual);
        }

    }
}
