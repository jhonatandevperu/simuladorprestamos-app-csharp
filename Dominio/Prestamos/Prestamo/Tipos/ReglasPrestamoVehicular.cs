#region Header

// Creado por: Christian
// Fecha: 04/06/2018 06:38
// Actualizado por ultima vez: 04/06/2018 06:38

#endregion

using System;
using System.Collections.Generic;

namespace Dominio.Prestamos.Prestamo.Tipos
{
    public class ReglasPrestamoVehicular : IReglasDelPrestamoService
    {
        /// <inheritdoc />
        public TipoPrestamo TipoPrestamo => TipoPrestamo.Vehicular;

        /// <inheritdoc />
        public decimal CostoEnvioACasa => 10;

        /// <inheritdoc />
        public decimal InteresMoratorio => 0.055M;

        /// <inheritdoc />
        public decimal TasaEfectivaAnual => 0.1449M;

        /// <inheritdoc />
        public decimal? TasaSeguroDelBien => 0.0435M;

        /// <inheritdoc />
        public decimal TasaSeguroDesgravamen => 0.000375M;

        /// <inheritdoc />
        public string NombreDelPrestamo => "Crédito Vehicular";

        /// <inheritdoc />
        public string NombreDelBien => "Vehículo";

        /// <inheritdoc />
        public decimal? CalcularSeguroDelBien(decimal importe)
        {
            return importe * (this.TasaSeguroDelBien/12);
        }

        /// <inheritdoc />
        public decimal ObtenerCuotaAjustada(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            return this._calculosFinancierosService.CalcularCuotaAjustada(importe,numeroCuotas,diasPorAnio,diasPorMes,this.TasaEfectivaAnual,this.TasaSeguroDesgravamen);
        }

        /// <inheritdoc />
        public decimal ObtenerMontoCuotaFijaMensual(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            return this.ObtenerCuotaAjustada(importe, numeroCuotas, diasPorAnio, diasPorMes) + this.CostoEnvioACasa + (decimal)this.CalcularSeguroDelBien(importe);
        }

        /// <inheritdoc />
        public Tuple<bool, IEnumerable<string>> EvaluarPrestamo(Prestatario.Prestatario prestatario,
                                                                decimal importe,
                                                                int numeroCuotas)
        {
            var reasons = new List<string>();
            
            if(importe < 20000 || importe > 150000)
            {
                reasons.Add("El importe no es valido para el prestamo");
            }

            if(prestatario.Edad < 24 || prestatario.Edad > 72)
            {
                reasons.Add("El prestatario no cumple con la edad necesaria para acceder a este tipo de prestamo.");
            }

            if(prestatario.IngresosNetosMensuales < 1500)
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
            for (var i = 0; i < numeroCuotas; i += 1)
            {
                //var dias = this._cuotasService.CalcularDiasHastaElSiguienteMes();
                var dias = diasPorMes; this._cuotasService.AñadirFechaSiguienteMes();
                var cuotaAjustada = this.ObtenerCuotaAjustada(saldo, totalCuotas, diasPorAnio, dias);
                var interes = this._calculosFinancierosService.ObtenerInteresMensual(saldo, diasPorAnio, dias, this.TasaEfectivaAnual);
                var desgravamen = this._calculosFinancierosService.ObtenerSeguroDesgravamenMensual(saldo, diasPorAnio, dias, this.TasaSeguroDesgravamen);
                var amortizacion = cuotaAjustada - interes - desgravamen;
                cuotas.Add(new Cuota(this.InteresMoratorio,
                                     i + 1,
                                     this._cuotasService.FechaActual,
                                     saldo,
                                     amortizacion,
                                     interes,
                                     desgravamen,
                                     (cuotaAjustada + this.CostoEnvioACasa + (decimal)this.CalcularSeguroDelBien(importe))));
                saldo -= amortizacion;
                totalCuotas--;
            }
            return cuotas;
        }

        private readonly CalculosFinancierosService _calculosFinancierosService;
        private readonly CuotasService _cuotasService;

        internal ReglasPrestamoVehicular(CalculosFinancierosService calculosFinancierosService,
                                         CuotasService cuotasService)
        {
            this._calculosFinancierosService = calculosFinancierosService;
            this._cuotasService = cuotasService;
        }
    }
}