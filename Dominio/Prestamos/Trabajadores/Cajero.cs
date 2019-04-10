#region Header
// Creado por: Christian
// Fecha: 31/05/2018 11:48
// Actualizado por ultima vez: 31/05/2018 11:48
#endregion

namespace Dominio.Prestamos.Trabajadores
{
    public class Cajero
    {
        public string NombreCompleto {get;}
        public string Identidad {get;}
        public int Id {get;}

        public Cajero(int id, string identidad, string nombreCompleto)
        {
            this.Id = id;
            this.Identidad = identidad;
            this.NombreCompleto = nombreCompleto;
        }
    }
}