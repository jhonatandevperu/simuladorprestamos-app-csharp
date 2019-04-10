#region Header
// Creado por: Christian
// Fecha: 15/05/2018 08:59
// Actualizado por ultima vez: 15/05/2018 08:59
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class SolicitudPrestamo
    {
        [DisplayName("DNI Cliente")]
        [Required]
        [MinLength(8, ErrorMessage = "El dni del cliente tiene solo 8 dígitos.")]
        [MaxLength(8, ErrorMessage = "El dni del cliente tiene solo 8 dígitos.")]
        public string DniCliente {get; set;}
        
        [DisplayName("Importe")]
        public decimal Importe {get; set;}

        [DisplayName("Numero de Cuotas")]
        [Required]
        public int NumeroCuotas {get; set;}

        public string[] TiposPrestamo {get; set;} = {"Efectivo", "Hipotecario", "Vehicular"};
        
        [Required]
        public string TipoPrestamo {get; set;}
    }
}