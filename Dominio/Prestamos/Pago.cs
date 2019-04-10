#region Header
// Creado por: Christian
// Fecha: 21/05/2018 18:47
// Actualizado por ultima vez: 21/05/2018 18:47
#endregion

using System;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos
{
    public class Pago
    {
        public decimal MontoCuota {get;}
        public decimal? MontoMora {get;}
        public decimal Importe {get;}
        public DateTime Fecha {get;}
        public string Codigo {get; protected set;}
        public Cajero Cajero {get;}

        public Pago(Cajero cajero, DateTime fecha, decimal montoCuota, decimal? montoMora)
        {
            this.Cajero = cajero;
            this.Fecha = fecha;
            this.MontoCuota = montoCuota;
            this.MontoMora = montoMora;
            this.Importe = montoCuota + (montoMora ?? 0);
            this.Codigo = Guid.NewGuid().ToString();
        }
    }
}