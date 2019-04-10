using System;

namespace Dominio.IdentidadAcceso.Identidad
{
    public class DescriptorUsuario
    {
        public string NombreUsuario {get; private set;}
        public string Nombre {get; private set;}

        public DescriptorUsuario(string nombreUsuario, string nombre)
        {
            if(nombreUsuario == null)
            {
                throw new Exception("El nombre de usuario no debe ser nulo.");
            }
            if(nombre == null)
            {
                throw new Exception("El nombre de usuario no debe ser nulo.");
            }

            this.NombreUsuario = nombreUsuario;
            this.Nombre = nombre;
        }
    }
}