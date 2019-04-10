using System;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;

namespace Aplicacion.CasosDeUso.RealizarPrestamo
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RealizarPrestamoService
    {
        private readonly PrestamosService _prestamosService;
        private readonly ITrabajadorPrestamoService _trabajadorPrestamoService;
        private readonly IPrestatarioService _prestatarioService;

        public RealizarPrestamoService(ITrabajadorPrestamoService trabajadorPrestamoService,
                                       PrestamosService prestamosService,
                                       IPrestatarioService prestatarioService)
        {
            this._prestamosService = prestamosService;
            this._trabajadorPrestamoService = trabajadorPrestamoService;
            this._prestatarioService = prestatarioService;
        }

        private DescriptorPrestamoOutput SolicitarPrestamo(TipoPrestamo tipoPrestamo,
                                                           SolicitarPrestamoInput input)
        {
            var analistaFinanciero = this._trabajadorPrestamoService.AnalistaFinancieroDesde(input.NombreUsuario);
            var prestatario = this._prestatarioService.PrestatarioDesde(input.DNICliente);
            return new DescriptorPrestamoOutput(this._prestamosService.SolicitarPrestamo(prestatario,
                                                                                         analistaFinanciero,
                                                                                         tipoPrestamo,
                                                                                         input.Importe,
                                                                                         input.NumeroCuotas,
                                                                                         input.FechaDesembolso,
                                                                                         input.DiaDePago));
        }

        private void RealizarPrestamo(TipoPrestamo tipoPrestamo, SolicitarPrestamoInput input)
        {
            var analistaFinanciero = this._trabajadorPrestamoService.AnalistaFinancieroDesde(input.NombreUsuario);
            var prestatario = this._prestatarioService.PrestatarioDesde(input.DNICliente);
            this._prestamosService.RealizarPrestamo(prestatario,
                                                    analistaFinanciero,
                                                    tipoPrestamo,
                                                    input.Importe,
                                                    input.NumeroCuotas,
                                                    input.FechaDesembolso,
                                                    input.DiaDePago);
        }

        public DescriptorPrestamoOutput SimularPrestamoEfectivo(SolicitarPrestamoInput input)
        {
            return this.SolicitarPrestamo(TipoPrestamo.Efectivo, input);
        }

        public void RealizarPrestamoEfectivo(SolicitarPrestamoInput input)
        {
            this.RealizarPrestamo(TipoPrestamo.Efectivo, input);
        }

        public void RealizarPrestamoVehicular(SolicitarPrestamoInput input)
        {
            this.RealizarPrestamo(TipoPrestamo.Vehicular, input);
        }

        public void RealizarPrestamoHipotecario(SolicitarPrestamoInput input)
        {
            this.RealizarPrestamo(TipoPrestamo.Hipotecario, input);
        }

        public DescriptorPrestamoOutput SimularCreditoHipotecario(SolicitarPrestamoInput input)
        {
            return this.SolicitarPrestamo(TipoPrestamo.Hipotecario, input);
        }

        public DescriptorPrestamoOutput SimularCreditoVehicular(SolicitarPrestamoInput input)
        {
            return this.SolicitarPrestamo(TipoPrestamo.Vehicular, input);
        }
    }
}