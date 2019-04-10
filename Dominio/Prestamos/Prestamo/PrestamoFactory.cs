#region Header

// Creado por: Christian
// Fecha: 04/06/2018 06:55
// Actualizado por ultima vez: 04/06/2018 06:55

#endregion

using System;
using Dominio.Prestamos.Prestamo.Tipos;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos.Prestamo
{
    public class PrestamoFactory
    {
        public virtual Prestamo Build(TipoPrestamo tipoPrestamo,
                                      decimal importe,
                                      int numeroCuotas,
                                      DateTime fechaDesembolso,
                                      int diaDePago,
                                      Prestatario.Prestatario prestatario,
                                      AnalistaFinanciero analistaFinanciero)
        {
            var cuotasService = new CuotasService(fechaDesembolso.Year, fechaDesembolso.Month, diaDePago);

            var calculosFinancierosService = new CalculosFinancierosService();

            IReglasDelPrestamoService reglas;

            switch(tipoPrestamo)
            {
                case TipoPrestamo.Efectivo:
                    reglas = new ReglasPrestamoEfectivo(calculosFinancierosService, cuotasService);
                    break;
                case TipoPrestamo.Vehicular:
                    reglas = new ReglasPrestamoVehicular(calculosFinancierosService, cuotasService);
                    break;
                case TipoPrestamo.Hipotecario:
                    reglas = new ReglasPrestamoHipotecario(calculosFinancierosService, cuotasService);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoPrestamo), tipoPrestamo, null);
            }

            return new Prestamo(reglas,
                                importe,
                                numeroCuotas,
                                fechaDesembolso,
                                diaDePago,
                                prestatario,
                                analistaFinanciero);
        }
    }
}