#region Header
// Creado por: Christian
// Fecha: 18/05/2018 23:46
// Actualizado por ultima vez: 18/05/2018 23:46
#endregion

namespace Dominio.Prestamos.Trabajadores
{
    public interface ITrabajadorPrestamoService
    {
        AnalistaFinanciero AnalistaFinancieroDesde(string identidad);
        AnalistaFinanciero AnalistaFinancieroDesde(int id);
        Cajero CajeroDesde(string identidad);
        Cajero CajeroDesde(int id);
    }
}