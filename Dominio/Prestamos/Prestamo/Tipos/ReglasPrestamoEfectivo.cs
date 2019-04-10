#region Header

// Creado por: Christian
// Fecha: 04/06/2018 06:23
// Actualizado por ultima vez: 04/06/2018 06:24

#endregion

using System;
using System.Collections.Generic;

namespace Dominio.Prestamos.Prestamo.Tipos
{
    public class ReglasPrestamoEfectivo : IReglasDelPrestamoService
    {
        /// <inheritdoc />
        public TipoPrestamo TipoPrestamo => TipoPrestamo.Efectivo;

        /// <inheritdoc />
        public virtual decimal CostoEnvioACasa => 10;

        /// <inheritdoc />
        public virtual decimal InteresMoratorio => 0.04M;

        /// <inheritdoc />
        public virtual decimal TasaEfectivaAnual => 0.14M;

        /// <inheritdoc />
        public virtual decimal? TasaSeguroDelBien => null;

        /// <inheritdoc />
        public virtual decimal TasaSeguroDesgravamen => 0.00075M;

        /// <inheritdoc />
        public virtual string NombreDelPrestamo => "Préstamo Efectivo";

        /// <inheritdoc />
        public virtual string NombreDelBien => null;

        /// <inheritdoc />
        public decimal? CalcularSeguroDelBien(decimal importe)
        {
            return null;
        }

        public decimal ObtenerCuotaAjustada(decimal importe,int numeroCuotas,int diasPorAnio,int diasPorMes)
        {
            return this._calculosFinancierosService.CalcularCuotaAjustada(importe,numeroCuotas,diasPorAnio,diasPorMes,this.TasaEfectivaAnual,this.TasaSeguroDesgravamen);
        }

        /// <inheritdoc />
        public decimal ObtenerMontoCuotaFijaMensual(decimal importe,int numeroCuotas,int diasPorAnio,int diasPorMes)
        {
            return this.ObtenerCuotaAjustada(importe, numeroCuotas, diasPorAnio, diasPorMes) + CostoEnvioACasa;
        }

        /// <inheritdoc />
        public virtual Tuple<bool, IEnumerable<string>> EvaluarPrestamo(Prestatario.Prestatario prestatario,
                                                                        decimal importe,
                                                                        int numeroCuotas)
        {
            var reasons = new List<string>();

            if(importe < 3000 || importe > 100000)
            {
                reasons.Add("El importe no es valido para el prestamo.");
            }

            if((!prestatario.EsDependiente || prestatario.Edad < 25) &&
               (prestatario.EsDependiente || prestatario.Edad < 21))
            {
                reasons.Add("El prestatario no cumple con la edad necesaria para acceder a este tipo de prestamo.");
            }

            if((!prestatario.EsDependiente || prestatario.IngresosNetosMensuales < 1500) &&
               (prestatario.EsDependiente || prestatario.IngresosNetosMensuales < 1000))
            {
                reasons.Add("El prestatario no cumple con los ingresos netos minimos requeridos.");
            }

            return new Tuple<bool, IEnumerable<string>>(reasons.Count == 0, reasons);
        }

        /// <inheritdoc />
        public List<Cuota> GenerarCuotas(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            var saldo = importe;
            var cuotas = new List<Cuota>();
            int totalCuotas = numeroCuotas;
            for(var i = 0; i < numeroCuotas; i += 1)
            {
                //var dias = this._cuotasService.CalcularDiasHastaElSiguienteMes();
                var dias = diasPorMes; this._cuotasService.AñadirFechaSiguienteMes();
                var cuotaAjustada = this.ObtenerCuotaAjustada(saldo,totalCuotas, diasPorAnio, dias);
                var interes = this._calculosFinancierosService.ObtenerInteresMensual(saldo,diasPorAnio,dias,this.TasaEfectivaAnual);
                var desgravamen = this._calculosFinancierosService.ObtenerSeguroDesgravamenMensual(saldo, diasPorAnio, dias, this.TasaSeguroDesgravamen);
                var amortizacion = cuotaAjustada - interes - desgravamen;
                cuotas.Add(new Cuota(this.InteresMoratorio,
                                     i + 1,
                                     this._cuotasService.FechaActual,
                                     saldo,
                                     amortizacion,
                                     interes,
                                     desgravamen,
                                     (cuotaAjustada + this.CostoEnvioACasa)));
                saldo -= amortizacion;
                totalCuotas--;
            }
            return cuotas;
        }

        private readonly CalculosFinancierosService _calculosFinancierosService;
        private readonly CuotasService _cuotasService;

        internal ReglasPrestamoEfectivo(CalculosFinancierosService calculosFinancierosService,
                                        CuotasService cuotasService)
        {
            this._calculosFinancierosService = calculosFinancierosService;
            this._cuotasService = cuotasService;
        }
    }
}