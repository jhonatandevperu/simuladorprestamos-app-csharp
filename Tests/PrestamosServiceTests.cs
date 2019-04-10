#region Header

// Creado por: Christian
// Fecha: 03/06/2018 08:33
// Actualizado por ultima vez: 03/06/2018 08:33

#endregion

using System;
using System.Collections.Generic;
using Dominio.Prestamos;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace Tests
{
    public class PrestamosServiceTests
    {
        private readonly IReglasDelPrestamoService _reglasDelPrestamoEfectivoService;

        public PrestamosServiceTests()
        {
            this._reglasDelPrestamoEfectivoService = Substitute.For<IReglasDelPrestamoService>();
            this._reglasDelPrestamoEfectivoService.CostoEnvioACasa.Returns(0);
            this._reglasDelPrestamoEfectivoService.InteresMoratorio.Returns(0.04M);
            this._reglasDelPrestamoEfectivoService.NombreDelBien.Returns((string)null);
            this._reglasDelPrestamoEfectivoService.NombreDelPrestamo.Returns("Prestamo Test");
            this._reglasDelPrestamoEfectivoService.TasaEfectivaAnual.Returns(0.14M);
            this._reglasDelPrestamoEfectivoService.TasaSeguroDesgravamen.Returns(0.00075M);
            this._reglasDelPrestamoEfectivoService.TasaSeguroDelBien.Returns((decimal?)null);
            this._reglasDelPrestamoEfectivoService.TipoPrestamo.Returns(TipoPrestamo.Efectivo);
        }

        [Fact]
        public void SolicitarPrestamoParaClienteSinPrestamoSinPagar()
        {
            var cuotaPagada = new Cuota(1, 1, DateTime.Now, 1000, 100, 10, 10, 1000);

            cuotaPagada.Pagar(new Cajero(1, "12345678", "John Doe"));

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaPagada});

            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        new DateTime(2018, 1, 1),
                                        1,
                                        new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>() {prestamo});

            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            var fecha = DateTime.Now;

            var sut = service.SolicitarPrestamo(new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                                new AnalistaFinanciero(1, "12345678", "John Doe"),
                                                TipoPrestamo.Efectivo,
                                                12000,
                                                24,
                                                fecha,
                                                fecha.Day);

            prestamoFactoryMock.ReceivedWithAnyArgs(1).Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);

            Assert.IsType<DescriptorPrestamo>(sut);
        }

        [Fact]
        public void SolicitarPrestamoParaClienteConPrestamoSinPagar()
        {
            var fecha = DateTime.Now.AddMonths(-2);

            var cuotaSinPagar = new Cuota(1, 1, fecha.AddMonths(1), 1000, 100, 10, 10, 1000);

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaSinPagar});

            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        fecha,
                                        1,
                                        new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>() {prestamo});

            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            Assert.Throws<Exception>(() =>
                                         service.SolicitarPrestamo(new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                                                   new AnalistaFinanciero(1, "12345678", "John Doe"),
                                                                   TipoPrestamo.Efectivo,
                                                                   12000,
                                                                   24,
                                                                   fecha,
                                                                   fecha.Day));

            prestamoFactoryMock.DidNotReceiveWithAnyArgs()
                               .Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);
        }

        [Fact]
        public void SolicitarPrestamoParaClienteSinPrestamo()
        {
            var cuotaPagada = new Cuota(1, 1, DateTime.Now, 1000, 100, 10, 10, 1000);

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaPagada});

            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        new DateTime(2018, 1, 1),
                                        1,
                                        new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>());

            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            var fecha = DateTime.Now;

            var sut = service.SolicitarPrestamo(new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                                new AnalistaFinanciero(1, "12345678", "John Doe"),
                                                TipoPrestamo.Efectivo,
                                                12000,
                                                24,
                                                fecha,
                                                fecha.Day);

            prestamoFactoryMock.ReceivedWithAnyArgs(1).Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);

            Assert.IsType<DescriptorPrestamo>(sut);
        }

        [Fact]
        public void RealizarPrestamoParaClienteConPrestamoSinPagar()
        {
            var fecha = DateTime.Now.AddMonths(-2);

            var cuotaSinPagar = new Cuota(1, 1, fecha.AddMonths(1), 1000, 100, 10, 10, 1000);

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaSinPagar});

            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        fecha,
                                        1,
                                        new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>() {prestamo});

            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            Assert.Throws<Exception>(() =>
                                         service.RealizarPrestamo(new Prestatario(1, "John Doe", 3000, 1000, true, 24),
                                                                  new AnalistaFinanciero(1, "12345678", "John Doe"),
                                                                  TipoPrestamo.Efectivo,
                                                                  12000,
                                                                  24,
                                                                  fecha,
                                                                  fecha.Day));

            prestamoFactoryMock.DidNotReceiveWithAnyArgs()
                               .Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.DidNotReceiveWithAnyArgs().RegistrarPrestamo(prestamo);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);
        }

        [Fact]
        public void RealizarPrestamoParaClienteSinPrestamoSinPagarNoValido()
        {
            var cuotaPagada = new Cuota(1, 1, DateTime.Now, 1000, 100, 10, 10, 1000);

            cuotaPagada.Pagar(new Cajero(1, "12345678", "John Doe"));

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaPagada});

            var prestatarioMock = Substitute.For<Prestatario>(1, "John Doe", 3000M, 1000M, true, 24);
            prestatarioMock.ObtenerCem().Returns(decimal.Zero);
            
            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        new DateTime(2018, 1, 1),
                                        1,
                                        prestatarioMock,
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>() {prestamo});


            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            var fecha = DateTime.Now;

            Assert.Throws<Exception>(() =>
                                         service.RealizarPrestamo(prestatarioMock,
                                                                  new AnalistaFinanciero(1, "12345678", "John Doe"),
                                                                  TipoPrestamo.Efectivo,
                                                                  12000,
                                                                  24,
                                                                  fecha,
                                                                  fecha.Day));


            prestamoFactoryMock.ReceivedWithAnyArgs(1).Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);
        }


        [Fact]
        public void RealizarPrestamoParaClienteSinPrestamoSinPagarValido()
        {
            var cuotaPagada = new Cuota(1, 1, DateTime.Now, 1000, 100, 10, 10, 1000);
            cuotaPagada.Pagar(new Cajero(1, "12345678", "John Doe"));

            this._reglasDelPrestamoEfectivoService.EvaluarPrestamo(null, 0, 0)
                .ReturnsForAnyArgs(new Tuple<bool, IEnumerable<string>>(true, null));
            this._reglasDelPrestamoEfectivoService.ObtenerMontoCuotaFijaMensual(0, 0, 0, 0).ReturnsForAnyArgs(1000);
            this._reglasDelPrestamoEfectivoService.GenerarCuotas(0, 0, 0, 0)
                .ReturnsForAnyArgs(new List<Cuota>() {cuotaPagada});

            var prestatarioMock = Substitute.For<Prestatario>(1, "John Doe", 3000M, 1000M, true, 24);
            prestatarioMock.ObtenerCem().Returns(decimal.MaxValue);

            var prestamo = new Prestamo(this._reglasDelPrestamoEfectivoService,
                                        12000,
                                        24,
                                        new DateTime(2018, 1, 1),
                                        1,
                                        prestatarioMock,
                                        new AnalistaFinanciero(1, "12345678", "John Doe"));

            var prestamoFactoryMock = Substitute.For<PrestamoFactory>();
            prestamoFactoryMock.Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 1, null, null)
                               .ReturnsForAnyArgs(prestamo);

            var prestamoRepositoryMock = Substitute.For<IPrestamoRepository>();

            prestamoRepositoryMock.PrestamosActivosDelPrestatario(1).Returns(new List<Prestamo>() {prestamo});

            var service = new PrestamosService(prestamoRepositoryMock, prestamoFactoryMock);

            var fecha = DateTime.Now;

            service.RealizarPrestamo(prestatarioMock,
                                     new AnalistaFinanciero(1, "12345678", "John Doe"),
                                     TipoPrestamo.Efectivo,
                                     12000,
                                     24,
                                     fecha,
                                     fecha.Day);

            prestamoFactoryMock.ReceivedWithAnyArgs(1).Build(TipoPrestamo.Efectivo, 0, 0, DateTime.Now, 0, null, null);
            prestamoRepositoryMock.ReceivedWithAnyArgs(1).PrestamosActivosDelPrestatario(1);
            prestamoRepositoryMock.Received(1).RegistrarPrestamo(prestamo);
        }
    }
}