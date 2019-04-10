#region Header

// Creado por: Christian
// Fecha: 14/05/2018 09:51
// Actualizado por ultima vez: 14/05/2018 09:51

#endregion

using System;

namespace Dominio.Prestamos
{
    public class CuotasService
    {
        public DateTime FechaActual {get; private set;}

        public CuotasService(int anioInicial, int mesInicial, int diaInicial) : this(new DateTime(anioInicial,
                                                                                                  mesInicial,
                                                                                                  diaInicial))
        {
        }

        private CuotasService(DateTime fechaInicial)
        {
            this.FechaActual = fechaInicial;
        }

        public int CalcularDiasHastaElSiguienteMes()
        {
            var siguienteFecha = this.FechaActual.AddMonths(1);
            var diff = (siguienteFecha - this.FechaActual).Days;
            this.FechaActual = siguienteFecha;
            return diff;
        }

        public void AñadirFechaSiguienteMes()
        {
            var siguienteFecha = this.FechaActual.AddMonths(1);
            this.FechaActual = siguienteFecha;
        }
    }
}