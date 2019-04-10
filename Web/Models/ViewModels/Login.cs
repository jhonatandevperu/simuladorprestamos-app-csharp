#region Header
// Creado por: Christian
// Fecha: 29/05/2018 08:48
// Actualizado por ultima vez: 29/05/2018 08:48
#endregion

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class Login
    {
        [Required]
        [DisplayName("Usuario")]
        public string NombreUsuario {get; set;}
        
        [Required]
        [DisplayName("Contraseña")]
        public string Clave {get; set;}
    }
}