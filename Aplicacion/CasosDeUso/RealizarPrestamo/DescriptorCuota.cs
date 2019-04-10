using System;
using Dominio.Prestamos;
using Dominio.Prestamos.Prestamo;

namespace Aplicacion.CasosDeUso.RealizarPrestamo
{
    public class DescriptorCuota
    {
        public int NumeroCuota {get;}
        public DateTime FechaVencimiento {get;}
        public decimal Saldo {get;}
        public decimal Amortizacion {get;}
        public decimal InteresMensual {get;}
        public decimal SeguroDesgravamenMensual {get;}
        public decimal CuotaFija {get;}
        public decimal? SeguroDelBien {get;}
        
        internal DescriptorCuota(CuotaSoloLectura cuota)
        {
            this.NumeroCuota = cuota.NumeroCuota;
            this.FechaVencimiento = cuota.FechaVencimiento;
            this.Saldo = cuota.SaldoInicial;
            this.Amortizacion = cuota.Amortizacion;
            this.InteresMensual = cuota.InteresMensual;
            this.SeguroDesgravamenMensual = cuota.SeguroDesgravamenMensual;
            this.CuotaFija = cuota.CuotaFija;
            this.SeguroDelBien = cuota.SeguroDelBien;
        }
    }
}