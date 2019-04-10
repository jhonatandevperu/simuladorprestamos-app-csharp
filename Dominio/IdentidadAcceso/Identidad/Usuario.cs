using System;
using Dominio.Comun;

namespace Dominio.IdentidadAcceso.Identidad
{
    public class Usuario : Entity
    {
        private string _nombreUsuario;

        public string NombreUsuario
        {
            get
            {
                return this._nombreUsuario;
            }
            protected set
            {
                if(value == null)
                {
                    throw new Exception("El nombre de usuario es requerido.");
                }

                if(value.Length <= 5 || value.Length >= 20)
                {
                    throw new Exception("El nombre de usuario debe ser de entre 5 a 20 caracteres.");
                }

                this._nombreUsuario = value;
            }
        }

        public Persona Persona {get; protected set;}

        public string Clave {get; protected set;}
        public DescriptorUsuario DescriptorUsuario => new DescriptorUsuario(this.NombreUsuario, this.Persona.NombreCompleto);

        public override bool Equals(object obj)
        {
            if(obj != null && this.GetType() == obj.GetType())
            {
                var usuario = (Usuario)obj;
                return this.NombreUsuario == usuario.NombreUsuario;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return 45217 * 296;
        }

        protected Usuario()
        {
            
        }
        
        internal Usuario(string nombreUsuario, string clave, Persona persona)
        {
            this.NombreUsuario = nombreUsuario;
            //TODO: Encrypt password
            this.Clave = clave;
            this.Persona = persona;
        }
    }
}