#region Header

// Creado por: Christian
// Fecha: 31/05/2018 12:33
// Actualizado por ultima vez: 31/05/2018 12:33

#endregion

using System;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;

namespace Aplicacion.CasosDeUso.RealizarPago
{
    public class RealizarPagoService
    {
        private readonly PrestamosService _prestamosService;
        private readonly ITrabajadorPrestamoService _trabajadorPrestamoService;
        private readonly IPrestatarioService _prestatarioService;

        public RealizarPagoService(ITrabajadorPrestamoService trabajadorPrestamoService,
                                   PrestamosService prestamosService,
                                   IPrestatarioService prestatarioService)
        {
            this._prestamosService = prestamosService;
            this._trabajadorPrestamoService = trabajadorPrestamoService;
            this._prestatarioService = prestatarioService;
        }

        public DescriptorPago RealizarPago(string nombreDeUsuario, string dniCliente, string codigoPrestamo, int cuota, String comando = "solo_mostrar")
        {
            var prestatario = this._prestatarioService.PrestatarioDesde(dniCliente);
            var cajero = this._trabajadorPrestamoService.CajeroDesde(nombreDeUsuario);

            if (String.Equals(comando, "solo_mostrar"))
            {
                return new DescriptorPago(this._prestamosService.RealizarPago(prestatario, cajero, codigoPrestamo, cuota), prestatario);
            }
            else if (String.Equals(comando, "guardar_bd"))
            {
                return new DescriptorPago(this._prestamosService.RealizarPago(prestatario, cajero, codigoPrestamo, cuota, comando), prestatario);
            }
            else
            {
                throw new Exception("No se ha podido realizar el pago.");
            }

        }

        public RealizarPrestamo.DescriptorPrestamoOutput ObtenerPrestamoConCuotasPendientes(
            string dniCliente,
            string codigoPrestamo)
        {
            var prestatario = this._prestatarioService.PrestatarioDesde(dniCliente);
            return new RealizarPrestamo.DescriptorPrestamoOutput(this._prestamosService
                                                                     .ObtenerPrestamoConCuotasPendientes(prestatario,
                                                                                                         codigoPrestamo));
        }
    }
}