#region Header

// Creado por: Christian
// Fecha: 02/06/2018 10:08
// Actualizado por ultima vez: 02/06/2018 10:08

#endregion

using System;
using Xunit;

namespace Dominio.Prestamos.Tests
{
    public class CuotasServiceTests
    {
        [Theory]
        [InlineData(2018, 1, 1, 31)]
        [InlineData(2018, 4, 1, 30)]
        public void CalcularDiasHastaElSiguienteMesTest(int anio, int mes, int dia, int diasEsperados)
        {
            var servicio = new CuotasService(anio, mes, dia);
            Assert.Equal(diasEsperados, servicio.CalcularDiasHastaElSiguienteMes());
        }
    }
}