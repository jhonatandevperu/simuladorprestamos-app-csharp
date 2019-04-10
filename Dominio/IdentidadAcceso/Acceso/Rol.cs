using Dominio.Comun;

namespace Dominio.IdentidadAcceso.Acceso
{
    public class Rol : Entity
    {
        public string Nombre {get; protected set;}

        protected Rol()
        {
            
        }
        
        internal Rol(string nombre)
        {
            this.Nombre = nombre;
        }
    }
}