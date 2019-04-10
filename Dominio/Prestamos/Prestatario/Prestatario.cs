namespace Dominio.Prestamos.Prestatario
{
    public class Prestatario
    {
        public int Id {get;}
        public string NombreCompleto {get;}
        public decimal IngresosBrutosMensuales {get;}
        public decimal GastosFijosMensuales {get;}
        public bool EsDependiente {get;}
        public int Edad {get;}

        public decimal IngresosNetosMensuales => this.IngresosBrutosMensuales - this.GastosFijosMensuales;

        public Prestatario(int id, string nombreCompleto, decimal ingresosBrutosMensuales, decimal gastosFijosMensuales, bool esDependiente, int edad)
        {
            this.Id = id;
            this.NombreCompleto = nombreCompleto;
            this.IngresosBrutosMensuales = ingresosBrutosMensuales;
            this.GastosFijosMensuales = gastosFijosMensuales;
            this.EsDependiente = esDependiente;
            this.Edad = edad;
        }

        public virtual decimal ObtenerCem()
        {
            return this.IngresosNetosMensuales * 0.35M;
        }
    }
}