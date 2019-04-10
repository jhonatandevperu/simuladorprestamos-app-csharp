using System;

namespace Dominio.Comun
{
    public static class UtilidadesService
    {
        public static int DiffInYearsBetweenDates(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                   (((end.Month > start.Month) || ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
        public static int DiffInDays(this DateTime end, DateTime start)
        {
            return (end - start).Days;
        }
    }
}