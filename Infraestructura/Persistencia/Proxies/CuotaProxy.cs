#region Header

// Creado por: Christian
// Fecha: 21/05/2018 15:57
// Actualizado por ultima vez: 21/05/2018 15:57

#endregion

using System;
using Dominio.Prestamos;
using Dominio.Prestamos.Trabajadores;

namespace Infraestructura.Persistencia.Proxies
{
    public class CuotaProxy : Cuota
    {
        internal CuotaProxy(int id,
                            decimal interesPorMora,
                            int numeroCuota,
                            DateTime fechaVencimiento,
                            decimal saldoInicial,
                            decimal amortizacion,
                            decimal interesMensual,
                            decimal seguroDesgravamenMensual,
                            decimal cuotaFija,
                            decimal? seguroDelBien = null)
        {
            this.InteresMoratorio = interesPorMora;
            this.NumeroCuota = numeroCuota;
            this.FechaVencimiento = fechaVencimiento;
            this.SaldoInicial = saldoInicial;
            this.Amortizacion = amortizacion;
            this.InteresMensual = interesMensual;
            this.SeguroDesgravamenMensual = seguroDesgravamenMensual;
            this.CuotaFija = cuotaFija;
            this.SeguroDelBien = seguroDelBien;
        }

        internal void AgregarPago(Cajero cajero, DateTime fecha, string codigo, decimal? mora)
        {
            this.Pago = new PagoProxy(cajero, fecha, codigo, this.CuotaFija, mora);
        }
    }
}