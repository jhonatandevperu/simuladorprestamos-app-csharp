using System;
using Dominio.Comun;
using Dominio.Prestamos;
using Dominio.Prestamos.Prestatario;

namespace Dominio.Clientes
{
    public class Cliente : Entity
    {
        public string Nombres {get;}
        public string Apellidos {get;}
        public string Dni {get;}
        public decimal IngresosBrutosMensuales {get;}
        public decimal GastosFijosMensuales {get;}
        public TipoCliente TipoCliente {get;}
        public DateTime FechaNacimiento {get;}

        public string NombreCompleto => this.Apellidos + ", " + this.Nombres;

        public Cliente(string nombres,
                       string apellidos,
                       string dni,
                       decimal ingresosBrutosMensuales,
                       decimal gastosFijosMensuales,
                       TipoCliente tipoCliente,
                       DateTime fechaNacimiento)
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Dni = dni;
            this.IngresosBrutosMensuales = ingresosBrutosMensuales;
            this.GastosFijosMensuales = gastosFijosMensuales;
            this.TipoCliente = tipoCliente;
            this.FechaNacimiento = fechaNacimiento;
        }
    }
}