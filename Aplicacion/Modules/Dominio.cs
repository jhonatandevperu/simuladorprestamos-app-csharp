#region Header
// Creado por: Christian
// Fecha: 30/05/2018 12:53
// Actualizado por ultima vez: 30/05/2018 12:53
#endregion

using Autofac;
using Dominio.IdentidadAcceso.Acceso;
using Dominio.IdentidadAcceso.Identidad;
using Dominio.Prestamos.Prestamo;

namespace Aplicacion.Modules
{
    public class Dominio : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PrestamoFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PrestamosService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AutenticacionService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<AutorizacionService>().AsSelf().InstancePerLifetimeScope();
            //TODO: CuotaService
        }
    }
}