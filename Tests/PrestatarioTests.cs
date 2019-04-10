#region Header
// Creado por: Christian
// Fecha: 31/05/2018 14:51
// Actualizado por ultima vez: 31/05/2018 14:51
#endregion

using Xunit;

// ReSharper disable once CheckNamespace
namespace Dominio.Prestamos.Prestatario.Tests
{
    public class PrestatarioTests
    {
        [Fact]
        public void CalculoCorrectoDelIngresoNetoMensual()
        {
            var prestatario = new Prestatario(1, "placeholder", 1000, 300, true, 20);
            Assert.Equal(700, prestatario.IngresosNetosMensuales);
        }

		[Fact]
		public void CalculoCorrectoDelCem()
		{
			var prestatario = new Prestatario(1, "placeholder", 1000, 300, true, 20);
			Assert.Equal(245, prestatario.ObtenerCem(), 2);
		}
	}
}