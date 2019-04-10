#region Header
// Creado por: Christian
// Fecha: 01/06/2018 18:04
// Actualizado por ultima vez: 01/06/2018 18:04
#endregion

using Dominio.Prestamos.Prestamo;
using Xunit;

namespace Dominio.Prestamos.Prestatario.Tests
{
    public class CalculosFinancierosServiceTests
    {
        [Fact]
        public void ObtenerInteresMensualTest()
        {
            var servicio = new CalculosFinancierosService();
            Assert.Equal(131.746227349902000M, servicio.ObtenerInteresMensual(12000, 360, 30, 0.14M), 15);
        }

        [Fact]
        public void ObtenerSeguroDesgravamenMensualTest()
        {
            var servicio = new CalculosFinancierosService();
            Assert.Equal(9.0M, servicio.ObtenerSeguroDesgravamenMensual(12000, 360, 30, 0.00075M));
        }

        [Fact]
        public void CalcularCuotaFijaMensualReferencialTest()
        {
            var servicio = new CalculosFinancierosService();
            Assert.Equal(576.57772326423768272740915320M, servicio.CalcularCuotaAjustada(12000, 24, 360, 30, 0.14M, 0.00075M));
        }
    }
}