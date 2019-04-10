using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Prestamos;
using Dominio.Prestamos.Trabajadores;

namespace Tests
{
    [TestClass]
    public class PagosTests
    {
        [TestMethod]
        public void Retorna377_258ComoPagoTotalDeUnaCuota()
        {
            Cuota cuota=new Cuota(Convert.ToDecimal(0.4),1,
                                  Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                                  Convert.ToDecimal(234.28),Convert.ToDecimal(32.54),
                                  Convert.ToDecimal(2.25),Convert.ToDecimal(269.47),null);

            decimal valorEsperado = Convert.ToDecimal(377.258) ;
            var valorObtenido = cuota.ObtenerPagoTotal();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }

        [TestMethod]
        public void ReturnaNuloSiUnaCuotaNoHaSidoPagadaAun()
        {
            Cajero cajero = new Cajero(1, "Cajero", "Mamani Suarez");
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                                  Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                                  Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                                  Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            cuota.Pagar(cajero);
            Assert.IsNotNull(cuota.Pago);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
    "La cuota ya esta pagada.")]
        public void ReturnaUnaExcepcionTipoExceptionSiUnaCuotaEstaPagada()
        {
            Cajero cajero = new Cajero(1, "Cajero", "Mamani Suarez");
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            cuota.Pagar(cajero);
            cuota.Pagar(cajero);
        }

        [TestMethod]
        public void ReturnaFalsoSiUnaCuotaNoEstaPagada()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                                  Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                                  Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                                  Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            bool valorObtenido = cuota.EstaPagada();
            bool valorEsperado = false;
            Assert.AreEqual(valorObtenido, valorEsperado);
        }

        [TestMethod]
        public void Returna50ComoMoraSiElTotalDeCuotaFijaPorInteresMoratorioPorDiasDeMoraEsMenorA50()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(50), null);
            decimal valorEsperado = Convert.ToDecimal(50);
            decimal valorObtenido = cuota.CalcularMora();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }
        [TestMethod]
        public void Returna107_788ComoMoraSiElTotalDeCuotaFijaPorInteresMoratorioPorDiasDeMoraEsMayorA50()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            decimal valorEsperado = Convert.ToDecimal(107.788);
            decimal valorObtenido = cuota.CalcularMora();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }
        [TestMethod]
        public void Returna0ComoMoraSiLaCuotaNoEstaVencida()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      DateTime.Now, Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            decimal valorEsperado = 0;
            decimal valorObtenido = cuota.CalcularMora();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }

        [TestMethod]
        public void RetornaVerdaderoSiLaCuotaNoEstaVencida()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      DateTime.Now, Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            bool valorEsperado = false;
            bool valorObtenido = cuota.EstaVencida();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }

        [TestMethod]
        public void RetornaFalsoSiLaCuotaEstaVencida()
        {
            Cuota cuota = new Cuota(Convert.ToDecimal(0.4), 1,
                      Convert.ToDateTime("2018-06-03"), Convert.ToDecimal(1000),
                      Convert.ToDecimal(234.28), Convert.ToDecimal(32.54),
                      Convert.ToDecimal(2.25), Convert.ToDecimal(269.47), null);
            bool valorEsperado = true;
            bool valorObtenido = cuota.EstaVencida();
            Assert.AreEqual(valorEsperado, valorObtenido);
        }


    }
}
