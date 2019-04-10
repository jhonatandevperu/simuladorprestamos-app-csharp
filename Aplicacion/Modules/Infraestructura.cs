#region Header

// Creado por: Christian
// Fecha: 30/05/2018 12:28
// Actualizado por ultima vez: 30/05/2018 12:28

#endregion

using Autofac;
using Infraestructura.Persistencia.Vanilla.Prestamos;
using Infraestructura.Servicios.Prestamos;

namespace Aplicacion.Modules
{
    public class Infraestructura : Autofac.Module
    {
        public string ConnectionString {get; set;}

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TraductorPrestatario>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TraductorTrabajadorPrestamo>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(PrestamoRepository).Assembly).AsImplementedInterfaces()
                   .WithParameter("connectionString", this.ConnectionString).InstancePerLifetimeScope();
        }
    }
}