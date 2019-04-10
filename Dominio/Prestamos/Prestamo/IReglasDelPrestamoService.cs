#region Header

// Creado por: Christian
// Fecha: 04/06/2018 06:13
// Actualizado por ultima vez: 04/06/2018 06:13

#endregion

using System;
using System.Collections.Generic;

namespace Dominio.Prestamos.Prestamo
{
    public interface IReglasDelPrestamoService
    {
        TipoPrestamo TipoPrestamo {get;}

        decimal CostoEnvioACasa {get;}

        decimal InteresMoratorio {get;}

        decimal TasaEfectivaAnual {get;}

        decimal? TasaSeguroDelBien {get;}

        decimal TasaSeguroDesgravamen {get;}

        string NombreDelPrestamo {get;}

        string NombreDelBien {get;}

        decimal? CalcularSeguroDelBien(decimal importe);

        decimal ObtenerCuotaAjustada(decimal importe,
                                           int numeroCuotas,
                                           int diasPorAnio,
                                           int diasPorMes);

        decimal ObtenerMontoCuotaFijaMensual(decimal importe,
                                             int numeroCuotas,
                                             int diasPorAnio,
                                             int diasPorMes);

        Tuple<bool, IEnumerable<string>> EvaluarPrestamo(Prestatario.Prestatario prestatario,
                                                         decimal importe,
                                                         int numeroCuotas);

        List<Cuota> GenerarCuotas(decimal importe, int numeroCuotas, int diasPorAnio, int diasPorMes);
    }
}