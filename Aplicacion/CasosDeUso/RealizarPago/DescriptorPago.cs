using System;
using Dominio.Prestamos;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;

namespace Aplicacion.CasosDeUso.RealizarPago
{
    public class DescriptorPago
    {
        public int Cuota_NumeroCuota { get; }
        public DateTime Cuota_FechaVencimiento { get; }
        public decimal Pago_MontoCuota { get; }
        public decimal? Pago_MontoMora { get; }
        public decimal Pago_Importe { get; }
        public DateTime Pago_Fecha { get; }
        public string Cajero_NombreCompleto { get; }
        public int Cajero_Id { get; }
        public int Prestatario_Id { get; }
        public string Prestatario_NombreCompleto { get; }

        internal DescriptorPago(CuotaSoloLectura cuota, Prestatario prestatario)
        {
            this.Cuota_NumeroCuota = cuota.NumeroCuota;
            this.Cuota_FechaVencimiento = cuota.FechaVencimiento;
            this.Pago_MontoCuota = cuota.Pago.MontoCuota;
            this.Pago_MontoMora = cuota.Pago.MontoMora;
            this.Pago_Importe = cuota.Pago.Importe;
            this.Pago_Fecha = cuota.Pago.Fecha;
            this.Cajero_Id = cuota.Pago.Cajero.Id;
            this.Cajero_NombreCompleto = cuota.Pago.Cajero.NombreCompleto;
            this.Prestatario_Id = prestatario.Id;
            this.Prestatario_NombreCompleto = prestatario.NombreCompleto;
        }
    }
}