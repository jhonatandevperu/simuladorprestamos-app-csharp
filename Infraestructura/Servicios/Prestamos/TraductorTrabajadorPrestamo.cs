#region Header
// Creado por: Christian
// Fecha: 21/05/2018 13:44
// Actualizado por ultima vez: 21/05/2018 13:44
#endregion

using Dominio.IdentidadAcceso.Identidad;
using Dominio.Prestamos.Trabajadores;

namespace Infraestructura.Servicios.Prestamos
{
    public class TraductorTrabajadorPrestamo
    {
        public AnalistaFinanciero AAnalistaFinancieroDesdeRepresentacion(Usuario usuario)
        {
            return new AnalistaFinanciero(usuario.Id, usuario.NombreUsuario, usuario.Persona.NombreCompleto);
        }

        public Cajero ACajeroDesdeRepresentacion(Usuario usuario)
        {
            return new Cajero(usuario.Id, usuario.NombreUsuario, usuario.Persona.NombreCompleto);
        }
    }
}