namespace Dominio.Prestamos.Trabajadores
{
    public class AnalistaFinanciero
    {
        public string NombreCompleto {get;}
        public string Identidad {get;}
        public int Id {get;}

        public AnalistaFinanciero(int id, string identidad, string nombreCompleto)
        {
            this.Id = id;
            this.Identidad = identidad;
            this.NombreCompleto = nombreCompleto;
        }
    }
}