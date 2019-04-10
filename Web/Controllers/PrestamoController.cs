#region Header

// Creado por: Christian
// Fecha: 15/05/2018 08:48
// Actualizado por ultima vez: 15/05/2018 08:48

#endregion

using System;
using System.Linq;
using System.Web.Mvc;
using Aplicacion.CasosDeUso.IngresarAlSistema;
using Aplicacion.CasosDeUso.RealizarPago;
using Aplicacion.CasosDeUso.RealizarPrestamo;
using Web.Models.ViewModels;
using ModelStateDictionary = System.Web.WebPages.Html.ModelStateDictionary;

namespace Web.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly RealizarPrestamoService _realizarPrestamoService;
        private readonly RealizarPagoService _realizarPagoService;

        public PrestamoController(RealizarPrestamoService realizarPrestamoService, RealizarPagoService realizarPagoService)
        {
            this._realizarPrestamoService = realizarPrestamoService;
            this._realizarPagoService = realizarPagoService;
        }

        public ActionResult Index(SolicitudPrestamo _model = null)
        {
            var model = _model ?? new SolicitudPrestamo();

            this.ViewBag.NombreAnalistaFinanciero = ((LoginOutput)this.Session["usuario"]).Nombre;

            return this.View(model);
        }

        [HttpPost]
        public ActionResult SolicitarDatos(SolicitudPrestamo model)
        {
            if (!this.ModelState.IsValidField("TipoPrestamo"))
            {
                return this.RedirectToAction("Index");
            }

            this.ViewBag.NombreAnalistaFinanciero = ((LoginOutput)this.Session["usuario"]).Nombre;

            foreach (var key in this.ModelState.Keys.ToList().Where(key => this.ModelState.ContainsKey(key)))
            {
                this.ModelState[key].Errors.Clear();
            }

            return this.View($"Solicitud{model.TipoPrestamo}", model);
        }

        [HttpPost]
        public ActionResult Simular(SolicitudPrestamo model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            var usuario = ((LoginOutput)this.Session["usuario"]).NombreUsuario;

            var solicitud =
                new SolicitarPrestamoInput(usuario, model.DniCliente, model.Importe, model.NumeroCuotas, DateTime.Now);


            DescriptorPrestamoOutput prestamoSimulado = null;

            switch (model.TipoPrestamo)
            {
                case "Efectivo":
                    prestamoSimulado = this._realizarPrestamoService.SimularPrestamoEfectivo(solicitud);
                    break;
                case "Vehicular":
                    prestamoSimulado = this._realizarPrestamoService.SimularCreditoVehicular(solicitud);
                    break;
                case "Hipotecario":
                    prestamoSimulado = this._realizarPrestamoService.SimularCreditoHipotecario(solicitud);
                    break;
                default:
                    break;
            }

            this.Session["solicitud_prestamo"] = solicitud;

            return this.View(prestamoSimulado);
        }

        [HttpPost]
        public ActionResult Realizar(string tipoPrestamo)
        {
            var solicitud = this.Session["solicitud_prestamo"] as SolicitarPrestamoInput;
            if (solicitud != null)
            {
                switch (tipoPrestamo)
                {
                    case "Efectivo":
                        this._realizarPrestamoService.RealizarPrestamoEfectivo(solicitud);
                        return InvocarAlerta("Préstamo Efectivo Guardado!", "Index", "Prestamo");
                    case "Vehicular":
                        this._realizarPrestamoService.RealizarPrestamoVehicular(solicitud);
                        return InvocarAlerta("Préstamo Vehicular Guardado!", "Index", "Prestamo");
                    case "Hipotecario":
                        this._realizarPrestamoService.RealizarPrestamoHipotecario(solicitud);
                        return InvocarAlerta("Préstamo Hipotecario Guardado!", "Index", "Prestamo");
                    default:
                        return InvocarAlerta("Préstamo Inválido, NO Guardado!", "Index", "Prestamo");
                }
            }
            else
            {
                throw new Exception();
            }
        }

        private ContentResult InvocarAlerta(string mensaje, string accion, string controlador)
        {
            string script = "<script language='javascript' type='text/javascript'>alert('" + mensaje + "');window.location.href =\"" + Url.Action(accion, controlador) + "\";</script>";
            return Content(script);

        }
    }
}