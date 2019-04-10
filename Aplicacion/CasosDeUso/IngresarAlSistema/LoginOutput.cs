#region Header

// Creado por: Christian
// Fecha: 29/05/2018 09:12
// Actualizado por ultima vez: 29/05/2018 09:12

#endregion

namespace Aplicacion.CasosDeUso.IngresarAlSistema
{
    public class LoginOutput
    {
        public string NombreUsuario {get;}
        public string Nombre {get;}

        public LoginOutput(string nombreUsuario, string nombre)
        {
            this.NombreUsuario = nombreUsuario;
            this.Nombre = nombre;
        }
    }
}