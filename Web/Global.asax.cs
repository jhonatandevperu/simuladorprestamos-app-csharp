using System.Web.Mvc;
using System.Web.Routing;
using Aplicacion.Modules;
using Autofac;
using Autofac.Integration.Mvc;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(CreateContainer()));
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterFilterProvider();
            builder.RegisterModule(new Infraestructura()
                                   {
                                       ConnectionString =
                                           "Data Source=DESKTOP-7UJFHRB\\SKL;Initial Catalog=prestamos;Integrated Security=True"
                                   });
            builder.RegisterModule<Dominio>();
            builder.RegisterModule<Aplicacion.Modules.Aplicacion>();
            return builder.Build();
        }
    }
}