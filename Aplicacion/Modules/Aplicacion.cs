#region Header

// Creado por: Christian
// Fecha: 30/05/2018 12:34
// Actualizado por ultima vez: 30/05/2018 12:34

#endregion

using Aplicacion.CasosDeUso.IngresarAlSistema;
using Aplicacion.CasosDeUso.RealizarPago;
using Aplicacion.CasosDeUso.RealizarPrestamo;
using Autofac;

namespace Aplicacion.Modules
{
    public class Aplicacion : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RealizarPrestamoService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RealizarPagoService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginService>().AsSelf().InstancePerLifetimeScope();
        }
    }
}