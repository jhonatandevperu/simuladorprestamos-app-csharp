#region Header

// Creado por: Christian
// Fecha: 31/05/2018 11:24
// Actualizado por ultima vez: 31/05/2018 11:24

#endregion

using System;
using Dominio.Prestamos;
using Dominio.Prestamos.Trabajadores;

namespace Infraestructura.Persistencia.Proxies
{
    public class PagoProxy : Pago
    {
        public PagoProxy(Cajero cajero, DateTime fecha, string codigo, decimal montoCuota, decimal? montoMora) :
            base(cajero, fecha, montoCuota, montoMora)
        {
            this.Codigo = codigo;
        }
    }
}