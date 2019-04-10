#region Header

// Creado por: Christian
// Fecha: 21/05/2018 14:48
// Actualizado por ultima vez: 21/05/2018 14:48

#endregion

using System;
using Dominio.Clientes;
using Dominio.Comun;
using Dominio.Prestamos.Prestatario;

namespace Infraestructura.Servicios.Prestamos
{
    public class TraductorPrestatario
    {
        public Prestatario APrestatarioDesdeRepresentacion(Cliente cliente)
        {
            return new Prestatario(cliente.Id,
                                   cliente.NombreCompleto,
                                   cliente.IngresosBrutosMensuales,
                                   cliente.GastosFijosMensuales,
                                   cliente.TipoCliente == TipoCliente.Dependiente,
                                   UtilidadesService.DiffInYearsBetweenDates(cliente.FechaNacimiento, DateTime.Now));
        }
    }
}