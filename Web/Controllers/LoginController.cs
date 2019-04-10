#region Header

// Creado por: Christian
// Fecha: 29/05/2018 08:51
// Actualizado por ultima vez: 29/05/2018 08:51

#endregion

using System.Web.Mvc;
using Aplicacion.CasosDeUso.IngresarAlSistema;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        
        public LoginController(LoginService loginService)
        {
            this._loginService = loginService;
        }
        
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Ingresar(Login model)
        {
            if(!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            var resultado = this._loginService.Login(model.NombreUsuario, model.Clave);

            if(resultado == null)
            {
                //TODO: indicar error al cliente.
                return null;
            }
            else
            {
                this.Session["usuario"] = resultado;
                return this.RedirectToAction("Index", "Home");
            }
        }
    }
}