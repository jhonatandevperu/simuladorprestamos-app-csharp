#region Header

// Creado por: Christian
// Fecha: 31/05/2018 06:23
// Actualizado por ultima vez: 31/05/2018 06:23

#endregion

using System;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;
using Dominio.Prestamos.Trabajadores;

namespace Infraestructura.Persistencia.Proxies
{
    public class PrestamoProxy : Prestamo
    {
        internal PrestamoProxy(ReglasPrestamoProxy reglas,
                               int id,
                               string codigo,
                               Prestatario prestatario,
                               AnalistaFinanciero analistaFinanciero,
                               int diasPorAnio,
                               int diasPorMes,
                               DateTime fechaDesembolso,
                               int diaDePago,
                               decimal importe,
                               int numeroCuotas,
                               decimal? valorDelBien)
        {
            this.ReglasDelPrestamoService = reglas;
            this.Id = id;
            this.Codigo = codigo;
            this.DiasPorAnio = diasPorAnio;
            this.DiasPorMes = diasPorMes;
            this.FechaDesembolso = fechaDesembolso;
            this.DiaDePago = diaDePago;
            this.Importe = importe;
            this.NumeroCuotas = numeroCuotas;
            this.ValorDelBien = valorDelBien;
            this.Prestatario = prestatario;
            this.AnalistaFinanciero = analistaFinanciero;
        }

        internal CuotaProxy AgregarCuota(int id,
                                         decimal interesMoratorio,
                                         int numeroCuota,
                                         DateTime fechaVencimiento,
                                         decimal saldo,
                                         decimal amortizacion,
                                         decimal interes,
                                         decimal desgravamen,
                                         decimal cuotaReferencial,
                                         decimal? seguroDelBien)
        {
            var cuota = new CuotaProxy(id,
                                       interesMoratorio,
                                       numeroCuota,
                                       fechaVencimiento,
                                       saldo,
                                       amortizacion,
                                       interes,
                                       desgravamen,
                                       cuotaReferencial,
                                       seguroDelBien);
            this.CuotasInternas.Add(cuota);
            return cuota;
        }
    }
}