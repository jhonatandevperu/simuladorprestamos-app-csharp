#region Header

// Creado por: Christian
// Fecha: 31/05/2018 11:29
// Actualizado por ultima vez: 31/05/2018 11:29

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using Dominio.Prestamos.Prestamo;

namespace Infraestructura.Persistencia.Vanilla.Prestamos
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class PrestamoModel
    {
        public int id;
        public string codigo;
        public TipoPrestamo tipo_prestamo;
        public decimal interes_moratorio;
        public int dias_por_anio;
        public int dias_por_mes;
        public DateTime fecha_desembolso;
        public int dia_de_pago;
        public decimal tea;
        public decimal tdes;
        public decimal importe;
        public int numero_cuotas;
        public decimal costo_envio_a_casa;
        public decimal? tasa_seguro_del_bien;
        public decimal? valor_del_bien;
        public int prestatario_id;
        public int analista_financiero_id;
        public string nombre_del_prestamo;
        public string nombre_del_bien;
        public decimal cuota_fija_mensual;
    }
}