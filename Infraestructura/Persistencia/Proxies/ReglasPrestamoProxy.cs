#region Header
// Creado por: Christian
// Fecha: 04/06/2018 10:43
// Actualizado por ultima vez: 04/06/2018 10:43
#endregion

using System;
using System.Collections.Generic;
using Dominio.Prestamos;
using Dominio.Prestamos.Prestamo;
using Dominio.Prestamos.Prestatario;

namespace Infraestructura.Persistencia.Proxies
{
    public class ReglasPrestamoProxy : IReglasDelPrestamoService
    {
        /// <inheritdoc />
        public TipoPrestamo TipoPrestamo {get;}

        /// <inheritdoc />
        public decimal CostoEnvioACasa {get;}

        /// <inheritdoc />
        public decimal InteresMoratorio {get;}

        /// <inheritdoc />
        public decimal TasaEfectivaAnual {get;}

        /// <inheritdoc />
        public decimal? TasaSeguroDelBien {get;}

        /// <inheritdoc />
        public decimal TasaSeguroDesgravamen {get;}

        /// <inheritdoc />
        public string NombreDelPrestamo {get;}

        /// <inheritdoc />
        public string NombreDelBien {get;}

        private decimal _cuotaFijaMensual;
        
        /// <inheritdoc />
        public decimal? CalcularSeguroDelBien(decimal importe)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public decimal ObtenerCuotaAjustada(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public decimal ObtenerMontoCuotaFijaMensual(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            return this._cuotaFijaMensual;
        }

        /// <inheritdoc />
        public Tuple<bool, IEnumerable<string>> EvaluarPrestamo(Prestatario prestatario, decimal importe, int numeroCuotas)
        {
            return new Tuple<bool, IEnumerable<string>>(true, new List<string>());
        }

        /// <inheritdoc />
        public List<Cuota> GenerarCuotas(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes)
        {
            throw new NotImplementedException();
        }

        internal ReglasPrestamoProxy(TipoPrestamo tipoPrestamo, string nombreDelPrestamo, decimal cuota_fija_mensual, decimal tasaEfectivaAnual, decimal tasaSeguroDesgravamen, decimal interesMoratorio, decimal costoEnvioACasa, string nombreDelBien = null, decimal? tasaSeguroDelBien = null)
        {
            this.TipoPrestamo = tipoPrestamo;
            this.TasaEfectivaAnual = tasaEfectivaAnual;
            this.TasaSeguroDesgravamen = tasaSeguroDesgravamen;
            this.NombreDelPrestamo = nombreDelPrestamo;
            this.InteresMoratorio = interesMoratorio;
            this.CostoEnvioACasa = costoEnvioACasa;
            this.NombreDelBien = nombreDelBien;
            this.TasaSeguroDelBien = tasaSeguroDelBien;
            
        }
    }
}