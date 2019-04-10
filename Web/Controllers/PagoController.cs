#region Header

// Creado por: Christian
// Fecha: 15/05/2018 08:48
// Actualizado por ultima vez: 15/05/2018 08:48

#endregion

using System.Web.Mvc;
using Aplicacion.CasosDeUso.IngresarAlSistema;
using Aplicacion.CasosDeUso.RealizarPago;
using Aplicacion.CasosDeUso.RealizarPrestamo;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class PagoController : Controller
    {
        private readonly RealizarPagoService _realizarPagoService;

        public PagoController(RealizarPrestamoService realizarPrestamoService, RealizarPagoService realizarPagoService)
        {
            this._realizarPagoService = realizarPagoService;
        }

        public ActionResult Index(SolicitudPago _model = null)
        {
            var model = _model ?? new SolicitudPago();

            this.ViewBag.NombreCajero = ((LoginOutput)this.Session["usuario"]).Nombre;

            return this.View(model);
        }

        [HttpPost]
        public ActionResult ObtenerPrestamoConCuotasPendientes(SolicitudPago model)
        {
            if(!this.ModelState.IsValidField("DniCliente"))
            {
                return InvocarAlerta("¡El DNI no tiene formato válido!", "Index", "Pago");
            }
            if (!this.ModelState.IsValidField("CodigoPrestamo"))
            {
                return InvocarAlerta("¡El código de préstamo no tiene formato válido!", "Index", "Pago");
            }
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            this.ViewBag.Dni_Cliente = model.DniCliente;

            var prestamoSimulado =
                this._realizarPagoService.ObtenerPrestamoConCuotasPendientes(model.DniCliente, model.CodigoPrestamo);

            ViewBag.MostrarMensaje = false;
            int numeroCoutas = 0;
            foreach (var item in prestamoSimulado.DescriptorCronograma)
                numeroCoutas++;
            if (numeroCoutas==0)
                return InvocarAlerta("¡Este préstamo ha sido cancelado totalmente!", "Index", "Pago");
            return this.View(prestamoSimulado);
        }

        [HttpGet]
        public ActionResult MostrarPago(string dniCliente, string codigoPrestamo, int numeroCuota)
        {
            var usuario = ((LoginOutput)this.Session["usuario"]).NombreUsuario;
            ViewBag.Dni_Cliente = dniCliente;
            ViewBag.Codigo_Prestamo = codigoPrestamo;
            return this.View(this._realizarPagoService.RealizarPago(usuario, dniCliente, codigoPrestamo, numeroCuota));
        }

        [HttpPost]
        public ActionResult RegistrarPago(string dniCliente, string codigoPrestamo, int numeroCuota)
        {
            var usuario = ((LoginOutput)this.Session["usuario"]).NombreUsuario;
            if (this._realizarPagoService.RealizarPago(usuario, dniCliente, codigoPrestamo, numeroCuota, "guardar_bd")!=null)
            {
                return InvocarAlerta("¡Pago Guardado!", "Index", "Pago");
            }
            return InvocarAlerta("¡El Pago NO ha sido Guardado!", "Index", "Pago");
        }

        private ContentResult InvocarAlerta(string mensaje, string accion, string controlador)
        {
            string script = "<script language='javascript' type='text/javascript'>alert('"+mensaje+"');window.location.href =\"" + Url.Action(accion, controlador) + "\";</script>";
            return Content(script);
        }

    }
}