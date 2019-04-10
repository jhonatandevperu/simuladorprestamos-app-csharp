#region Header
// Creado por: Christian
// Fecha: 18/05/2018 19:54
// Actualizado por ultima vez: 18/05/2018 19:54
#endregion

using System.Collections.Generic;

namespace Dominio.Prestamos.Prestamo
{
    public interface IPrestamoRepository
    {
        void RegistrarPrestamo(Prestamo prestamo);
        IEnumerable<Prestamo> PrestamosActivosDelPrestatario(int prestatarioId);
        Prestamo RecuperarPrestamoDelPrestatarioDeCodigo(int prestatarioId, string codigo);
        void RegistrarPago(Prestamo prestamo, int numeroCuota);
    }
}