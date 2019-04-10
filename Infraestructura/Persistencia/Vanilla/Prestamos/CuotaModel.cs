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
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class CuotaModel
    {
        public int id;
        public decimal interes_moratorio;
        public int numero_cuota;
        public DateTime fecha_vencimiento;
        public decimal saldo;
        public decimal amortizacion;
        public decimal interes_mensual;
        public decimal seguro_desgravamen_mensual;
        public decimal cuota_fija;
        public decimal? seguro_del_bien;
        public int prestamo_id;
        public int? pago_id;

        public PagoModel pago;
    }
}