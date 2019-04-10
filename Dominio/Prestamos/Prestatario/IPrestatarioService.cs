#region Header
// Creado por: Christian
// Fecha: 18/05/2018 23:46
// Actualizado por ultima vez: 18/05/2018 23:46
#endregion

namespace Dominio.Prestamos.Prestatario
{
    public interface IPrestatarioService
    {
        Prestamos.Prestatario.Prestatario PrestatarioDesde(string dni);
        Prestamos.Prestatario.Prestatario PrestatarioDesde(int dni);
    }
}