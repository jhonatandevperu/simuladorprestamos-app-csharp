using Dominio.IdentidadAcceso;
using Dominio.IdentidadAcceso.Identidad;

namespace Infraestructura.Persistencia.Proxies
{
    internal class UsuarioProxy : Usuario
    {
        internal UsuarioProxy(int id, string nombreUsuario, string clave, Persona persona)
        {
            this.Id = id;
            this.NombreUsuario = nombreUsuario;
            //No encryption here
            this.Clave = clave;
            this.Persona = persona;
        }
    }
}