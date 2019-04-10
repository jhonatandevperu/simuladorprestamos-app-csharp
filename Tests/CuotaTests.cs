#region Header

// Creado por: Christian
// Fecha: 02/06/2018 12:53
// Actualizado por ultima vez: 02/06/2018 12:53

#endregion

using System;
using Dominio.Prestamos.Trabajadores;
using Xunit;

namespace Dominio.Prestamos.Prestamo.Tests
{
    public class CuotaTests
    {
        [Fact]
        public void PagoEstablecidoEnCuotaPagada()
        {
            var cuota = new Cuota(1, 1, new DateTime(2018, 1, 1), 12000, 300, 0.1M, 0.1M, 400);

            cuota.Pagar(new Cajero(1, "12345678", "John Doe"));

            Assert.True(cuota.EstaPagada());
            Assert.NotNull(cuota.Pago);
        }

        [Fact]
        public void PagoNoEstablecidoEnCuotaNoPagada()
        {
            var cuota = new Cuota(1, 1, new DateTime(2018, 1, 1), 12000, 300, 0.1M, 0.1M, 400);

            Assert.False(cuota.EstaPagada());
            Assert.Null(cuota.Pago);
        }

        [Fact]
        public void VencidaAlDiaSiguienteDeLaFechaDeVencimientoSinPago()
        {
            var fechaVencimiento = DateTime.Now.AddDays(-1);

            var cuota = new Cuota(1, 1, fechaVencimiento, 12000, 300, 0.1M, 0.1M, 400);

            Assert.True(cuota.EstaVencida());
        }

        [Fact]
        public void NoVencidaElMismoDiaDeLaFechaDeVencimientoSinPago()
        {
            var fechaActual = DateTime.Now;

            var cuota = new Cuota(1, 1, fechaActual, 12000, 300, 0.1M, 0.1M, 400);

            Assert.False(cuota.EstaVencida());
        }

        [Fact]
        public void VencidaAlDiaSiguienteDeLaFechaDeVencimientoConPago()
        {
            var fechaVencimiento = DateTime.Now.AddDays(-1);

            var cuota = new Cuota(1, 1, fechaVencimiento, 12000, 300, 0.1M, 0.1M, 400);

            cuota.Pagar(new Cajero(1, "12345678", "John Doe"));

            Assert.True(cuota.EstaVencida());
        }

        [Fact]
        public void NoVencidaElMismoDiaDeLaFechaDeVencimientoConPago()
        {
            var fechaActual = DateTime.Now;

            var cuota = new Cuota(1, 1, fechaActual, 12000, 300, 0.1M, 0.1M, 400);

            cuota.Pagar(new Cajero(1, "12345678", "John Doe"));

            Assert.False(cuota.EstaVencida());
        }
    }
}