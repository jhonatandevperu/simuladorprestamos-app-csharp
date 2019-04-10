#region Header
// Creado por: Christian
// Fecha: 31/05/2018 13:03
// Actualizado por ultima vez: 31/05/2018 13:03
#endregion

using System;

namespace Dominio.Prestamos.Prestamo
{
    public class CuotaSoloLectura
    {
        internal CuotaSoloLectura(Cuota cuota)
        {
            this.InteresMoratorio = cuota.InteresMoratorio;
            this.NumeroCuota = cuota.NumeroCuota;
            this.FechaVencimiento = cuota.FechaVencimiento;
            this.SaldoInicial = cuota.SaldoInicial;
            this.Amortizacion = cuota.Amortizacion;
            this.InteresMensual = cuota.InteresMensual;
            this.SeguroDesgravamenMensual = cuota.SeguroDesgravamenMensual;
            this.CuotaFija = cuota.CuotaFija;
            this.SeguroDelBien = cuota.SeguroDelBien;
            this.Pago = cuota.Pago;
        }
        
        public int NumeroCuota {get;}
        public DateTime FechaVencimiento {get;}
        public decimal SaldoInicial {get;}
        public decimal Amortizacion {get;}
        public decimal InteresMensual {get;}
        public decimal SeguroDesgravamenMensual {get;}
        public decimal CuotaFija {get;}
        public decimal? SeguroDelBien {get;}

        public Pago Pago {get;}

        public decimal InteresMoratorio {get;}
    }
}