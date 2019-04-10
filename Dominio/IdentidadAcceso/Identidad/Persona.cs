using System;

namespace Dominio.IdentidadAcceso.Identidad
{
    public class Persona
    {
        protected Persona()
        {
            
        }
        
        public Persona(string nombres, string apellidos, string dni, DateTime fechaNacimiento)
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.DNI = dni;
            this.FechaNacimiento = fechaNacimiento;
        }

        public string NombreCompleto => $"{this.Apellidos}, {this.Nombres}";

        public string Nombres {get;}

        public string Apellidos {get;}

        public string DNI {get;}

        public DateTime FechaNacimiento {get;}
    }
}