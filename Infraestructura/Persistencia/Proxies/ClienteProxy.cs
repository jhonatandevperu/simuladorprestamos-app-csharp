using System;
using Dominio.Clientes;

namespace Infraestructura.Persistencia.Proxies
{
    public class ClienteProxy : Cliente
    {
        internal ClienteProxy(int id,
                              string nombres,
                              string apellidos,
                              string dni,
                              decimal ingresosNetosMensuales,
                              decimal gastosFijosMensuales,
                              TipoCliente tipoCliente,
                              DateTime fechaNacimiento) : base(nombres, apellidos, dni, ingresosNetosMensuales, gastosFijosMensuales, tipoCliente, fechaNacimiento)
        {
            this.Id = id;
        }
    }
}