using Dominio.IdentidadAcceso;
using Dominio.IdentidadAcceso.Acceso;

namespace Infraestructura.Persistencia.Proxies
{
    internal class RolProxy : Rol
    {
        internal RolProxy(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}