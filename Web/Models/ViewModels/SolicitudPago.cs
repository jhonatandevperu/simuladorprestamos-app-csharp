using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class SolicitudPago
    {
        [DisplayName("DNI Cliente")]
        //[Required]
        [MinLength(8, ErrorMessage = "El dni del cliente tiene solo 8 dígitos.")]
        [MaxLength(8, ErrorMessage = "El dni del cliente tiene solo 8 dígitos.")]
        public string DniCliente { get; set; }

        [DisplayName("Código Préstamo")]
        [RegularExpression(@"[a-zA-Z0-9]{8}-([a-zA-Z0-9]{4}-){3}[a-zA-Z0-9]{12}", ErrorMessage ="Ingrese código de préstamo válido")]
        //[Required]
        public string CodigoPrestamo { get; set; }
    }
}