using System;
using System.Web.Mvc;
using Aplicacion.CasosDeUso.IngresarAlSistema;
using Aplicacion.CasosDeUso.RealizarPrestamo;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var usuario = this.Session["usuario"] as LoginOutput;
            
            if(usuario != null)
            {
                this.ViewBag.NombreTrabajador = usuario.Nombre;
                return this.View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}