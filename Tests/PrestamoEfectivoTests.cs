#region Header

// Creado por: Christian
// Fecha: 02/06/2018 11:10
// Actualizado por ultima vez: 02/06/2018 11:10

#endregion

using System;
using System.Collections.Generic;
using Dominio.Prestamos.Trabajadores;
using NSubstitute;
using Xunit;

namespace Dominio.Prestamos.Prestamo.Tests
{
    public class PrestamoEfectivoTests
    {
        [Fact]
        public void ElEstadoEsElCorrectoAlCrearElPrestamo()
        {
            const int costoEnvioACasa = 0;
            const decimal interesMoratorio = 0.04M;
            const string nombreDelBien = "Test";
            const string nombreDelPrestamo = "Prestamo Test";
            const decimal tasaEfectivaAnual = 0.14M;
            const decimal tasaSeguroDesgravamen = 0.00075M;

            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.CostoEnvioACasa.Returns(costoEnvioACasa);
            reglasDelPrestamoServiceMock.InteresMoratorio.Returns(interesMoratorio);
            reglasDelPrestamoServiceMock.NombreDelBien.Returns(nombreDelBien);
            reglasDelPrestamoServiceMock.NombreDelPrestamo.Returns(nombreDelPrestamo);
            reglasDelPrestamoServiceMock.TasaEfectivaAnual.Returns(tasaEfectivaAnual);
            reglasDelPrestamoServiceMock.TasaSeguroDesgravamen.Returns(tasaSeguroDesgravamen);
            reglasDelPrestamoServiceMock.TasaSeguroDelBien.Returns((decimal?)null);
            reglasDelPrestamoServiceMock.TipoPrestamo.Returns(TipoPrestamo.Efectivo);
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            reglasDelPrestamoServiceMock.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);

            reglasDelPrestamoServiceMock.GenerarCuotas(0, 0, 0, 0)
                                        .ReturnsForAnyArgs(new List<Cuota>()
                                                           {
                                                               new Cuota(0,
                                                                         1,
                                                                         DateTime.Now,
                                                                         1000,
                                                                         100,
                                                                         10,
                                                                         10,
                                                                         1000)
                                                           });
            const int importe = 12000;
            const int cuotas = 24;
            var fecha = DateTime.Now;

            var sut = new Prestamo(reglasDelPrestamoServiceMock,
                                   importe,
                                   cuotas,
                                   fecha,
                                   fecha.Day,
                                   new Prestatario.Prestatario(1, "John Doe", 3000, 1000, true, 30),
                                   new AnalistaFinanciero(1, "12345678", "John Doe"));

            Assert.Equal(TipoPrestamo.Efectivo, sut.TipoPrestamo);
            Assert.Equal(importe, sut.Importe);
            Assert.Equal(cuotas, sut.NumeroCuotas);
            Assert.Equal(fecha, sut.FechaDesembolso);
            Assert.Equal(fecha.Day, sut.DiaDePago);
            Assert.NotNull(sut.Prestatario);
            Assert.NotNull(sut.AnalistaFinanciero);
            Assert.Equal(tasaEfectivaAnual, sut.Tea);
            Assert.Equal(tasaSeguroDesgravamen, sut.Tdes);
            Assert.Null(sut.TasaSeguroDelBien);
            Assert.Null(sut.ValorDelBien);
            Assert.Equal(interesMoratorio, sut.InteresMoratorio);
            Assert.Equal(costoEnvioACasa, sut.CostoEnvioACasa);

            Assert.NotEmpty(sut.Cuotas);
            Assert.NotEmpty(sut.CuotasPendientes);

            Assert.NotNull(sut.DescriptorPrestamo);
            Assert.NotNull(sut.DescriptorPrestamoCuotasPendientes);

            Assert.Equal(0, sut.Id);
        }

        [Fact]
        public void ElEstadoDeLasCuotasGeneradasEsElCorrectoAlCrearElPrestamo()
        {
            const int costoEnvioACasa = 0;
            const decimal interesMoratorio = 0.04M;
            const string nombreDelBien = "Test";
            const string nombreDelPrestamo = "Prestamo Test";
            const decimal tasaEfectivaAnual = 0.14M;
            const decimal tasaSeguroDesgravamen = 0.00075M;

            const int importe = 12000;
            const int cuotas = 24;
            var fecha = DateTime.Now;

            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.CostoEnvioACasa.Returns(costoEnvioACasa);
            reglasDelPrestamoServiceMock.InteresMoratorio.Returns(interesMoratorio);
            reglasDelPrestamoServiceMock.NombreDelBien.Returns(nombreDelBien);
            reglasDelPrestamoServiceMock.NombreDelPrestamo.Returns(nombreDelPrestamo);
            reglasDelPrestamoServiceMock.TasaEfectivaAnual.Returns(tasaEfectivaAnual);
            reglasDelPrestamoServiceMock.TasaSeguroDesgravamen.Returns(tasaSeguroDesgravamen);
            reglasDelPrestamoServiceMock.TasaSeguroDelBien.Returns((decimal?)null);
            reglasDelPrestamoServiceMock.TipoPrestamo.Returns(TipoPrestamo.Efectivo);
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            reglasDelPrestamoServiceMock.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);

            reglasDelPrestamoServiceMock.GenerarCuotas(0, 0, 0, 0)
                                        .ReturnsForAnyArgs(new List<Cuota>()
                                                           {
                                                               new Cuota(1,
                                                                         1,
                                                                         fecha.AddMonths(1),
                                                                         1000,
                                                                         100,
                                                                         10,
                                                                         10,
                                                                         1000)
                                                           });

            var sut = new Prestamo(reglasDelPrestamoServiceMock,
                                   importe,
                                   cuotas,
                                   fecha,
                                   fecha.Day,
                                   new Prestatario.Prestatario(1, "John Doe", 3000, 1000, true, 30),
                                   new AnalistaFinanciero(1, "12345678", "John Doe"));

            Assert.All(sut.Cuotas,
                       c =>
                       {
                           Assert.IsType<CuotaSoloLectura>(c);
                           Assert.Null(c.Pago);
                           Assert.Null(c.SeguroDelBien);

                           Assert.InRange(c.NumeroCuota, 1, cuotas);
                           Assert.True(c.Amortizacion > 0);
                           Assert.True(c.CuotaFija > 0);
                           Assert.True(c.FechaVencimiento >= fecha.AddMonths(1));
                           Assert.True(c.InteresMensual > 0);
                           Assert.True(c.InteresMoratorio > 0);
                           Assert.True(c.SeguroDesgravamenMensual > 0);
                       });
        }

        [Fact]
        public void SoloElSaldoDeLaUltimaCuotaDebeSerCero()
        {
            const int costoEnvioACasa = 0;
            const decimal interesMoratorio = 0.04M;
            const string nombreDelBien = "Test";
            const string nombreDelPrestamo = "Prestamo Test";
            const decimal tasaEfectivaAnual = 0.14M;
            const decimal tasaSeguroDesgravamen = 0.00075M;

            const int importe = 12000;
            const int cuotas = 24;
            var fecha = DateTime.Now;

            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.CostoEnvioACasa.Returns(costoEnvioACasa);
            reglasDelPrestamoServiceMock.InteresMoratorio.Returns(interesMoratorio);
            reglasDelPrestamoServiceMock.NombreDelBien.Returns(nombreDelBien);
            reglasDelPrestamoServiceMock.NombreDelPrestamo.Returns(nombreDelPrestamo);
            reglasDelPrestamoServiceMock.TasaEfectivaAnual.Returns(tasaEfectivaAnual);
            reglasDelPrestamoServiceMock.TasaSeguroDesgravamen.Returns(tasaSeguroDesgravamen);
            reglasDelPrestamoServiceMock.TasaSeguroDelBien.Returns((decimal?)null);
            reglasDelPrestamoServiceMock.TipoPrestamo.Returns(TipoPrestamo.Efectivo);
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            reglasDelPrestamoServiceMock.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);

            reglasDelPrestamoServiceMock.GenerarCuotas(0, 0, 0, 0)
                                        .ReturnsForAnyArgs(new List<Cuota>()
                                                           {
                                                               new Cuota(1,
                                                                         1,
                                                                         fecha.AddMonths(1),
                                                                         1000,
                                                                         100,
                                                                         10,
                                                                         10,
                                                                         1000),
                                                               new Cuota(1,
                                                                         1,
                                                                         fecha.AddMonths(2),
                                                                         0,
                                                                         100,
                                                                         10,
                                                                         10,
                                                                         1000),
                                                           });

            var sut = new Prestamo(reglasDelPrestamoServiceMock,
                                   importe,
                                   cuotas,
                                   fecha,
                                   fecha.Day,
                                   new Prestatario.Prestatario(1, "John Doe", 3000, 1000, true, 30),
                                   new AnalistaFinanciero(1, "12345678", "John Doe"));

            Assert.All(sut.Cuotas,
                       c =>
                       {
                           Assert.IsType<CuotaSoloLectura>(c);
                           Assert.Null(c.Pago);
                           Assert.Null(c.SeguroDelBien);

                           Assert.InRange(c.NumeroCuota, 1, cuotas);
                           Assert.True(c.Amortizacion > 0);
                           Assert.True(c.CuotaFija > 0);
                           Assert.True(c.FechaVencimiento >= fecha.AddMonths(1));
                           Assert.True(c.InteresMensual > 0);
                           Assert.True(c.InteresMoratorio > 0);
                           Assert.True(c.SeguroDesgravamenMensual > 0);
                       });

            Assert.Single(sut.Cuotas, c => c.SaldoInicial == 0);
        }

        [Fact]
        public void DebeLanzarExcepcionAlNoCumplirLosRequisitosDelPrestamo()
        {
            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(false, null));
            const int importe = 12000;
            const int cuotas = 24;
            var fecha = DateTime.Now;

            Assert.Throws<Exception>(() => new Prestamo(reglasDelPrestamoServiceMock,
                                                        importe,
                                                        cuotas,
                                                        fecha,
                                                        fecha.Day,
                                                        new Prestatario.Prestatario(1,
                                                                                    "John Doe",
                                                                                    3000,
                                                                                    1000,
                                                                                    true,
                                                                                    30),
                                                        new AnalistaFinanciero(1, "12345678", "John Doe")));
        }

        [Fact]
        public void DebeLanzarUnaExcepcionAlPasarNullEnElPrestatario()
        {
            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(false, null));

            var fecha = DateTime.Now;

            Assert.Throws<Exception>(() => new Prestamo(reglasDelPrestamoServiceMock,
                                                        12000,
                                                        24,
                                                        fecha,
                                                        fecha.Day,
                                                        null,
                                                        new AnalistaFinanciero(1, "12345678", "John Doe")));
        }

        [Fact]
        public void DebeLanzarUnaExcepcionAlPasarNullEnElAnalistaFinanciero()
        {
            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(false, null));

            var fecha = DateTime.Now;

            Assert.Throws<Exception>(() => new Prestamo(reglasDelPrestamoServiceMock,
                                                        12000,
                                                        24,
                                                        fecha,
                                                        fecha.Day,
                                                        new Prestatario.Prestatario(1,
                                                                                    "John Doe",
                                                                                    3000,
                                                                                    1000,
                                                                                    true,
                                                                                    30),
                                                        null));
        }

        [Fact]
        public void DebeLanzarUnaExcepcionAlPasarNullEnLasReglasDelPrestamo()
        {
            var reglasDelPrestamoServiceMock = Substitute.For<IReglasDelPrestamoService>();
            reglasDelPrestamoServiceMock.EvaluarPrestamo(null, 0, 0)
                                        .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(false, null));

            var fecha = DateTime.Now;

            Assert.Throws<Exception>(() => new Prestamo(null,
                                                        12000,
                                                        24,
                                                        fecha,
                                                        fecha.Day,
                                                        new Prestatario.Prestatario(1,
                                                                                    "John Doe",
                                                                                    3000,
                                                                                    1000,
                                                                                    true,
                                                                                    30),
                                                        new AnalistaFinanciero(1, "12345678", "John Doe")));
        }
    }
}