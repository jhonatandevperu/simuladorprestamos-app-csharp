#region Header

// Creado por: Christian
// Fecha: 01/06/2018 12:37
// Actualizado por ultima vez: 01/06/2018 12:37

#endregion

using System;

namespace Dominio.Prestamos.Prestamo
{
    public class CalculosFinancierosService
    {
        private decimal calcularTasaEfectivaDiaria(int diasPorAnio, decimal tea) //TED
        {
            return (decimal)(Math.Pow((1 + (double)tea), (1 / (double)diasPorAnio)) - 1);
        }

        private decimal calcularTasaEfectivaMensual(int numeroDiasDelMes, decimal ted) //TEM
        {
            return (decimal)(Math.Pow((1 + (double)ted), (numeroDiasDelMes)) - 1);
        }

        public decimal ObtenerInteresMensual(decimal saldo, int diasPorAnio, int numeroDiasDelMes, decimal tea)
        {
            decimal ted = calcularTasaEfectivaDiaria(diasPorAnio, tea);
            return saldo * calcularTasaEfectivaMensual(numeroDiasDelMes,ted);
        }

        public decimal ObtenerSeguroDesgravamenMensual(decimal saldo,
                                                       int diasPorAnio,
                                                       int numeroDiasDelMes,
                                                       decimal tdes)
        {
            return saldo * tdes * (numeroDiasDelMes/30);
        }

        private decimal calcularTasaPrestamo(decimal tem, decimal tdes)
        {
            return tem + tdes;
        }
        
        public decimal CalcularCuotaAjustada(decimal saldo, int numeroCuotas, int diasPorAnio, int numeroDiasDelMes, decimal tea, decimal tdes)
        {
            decimal ted = calcularTasaEfectivaDiaria(diasPorAnio, tea);
            decimal tem = calcularTasaEfectivaMensual(numeroDiasDelMes, ted);
            decimal sdes = ObtenerSeguroDesgravamenMensual(saldo, diasPorAnio, numeroDiasDelMes, tdes);
            decimal tp = calcularTasaPrestamo(tem, tdes);
            return saldo * (decimal)((double)tp / (1 - Math.Pow((1 + (double)tp), -(numeroCuotas))));
        }
    }
}