#region Header
// Creado por: Christian
// Fecha: 31/05/2018 11:29
// Actualizado por ultima vez: 31/05/2018 11:29
#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructura.Persistencia.Vanilla.Prestamos
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class PagoModel
    {
        public int id;
        public string codigo;
        public DateTime fecha;
        public decimal? monto_mora;
        public int cajero_id;
    }
}