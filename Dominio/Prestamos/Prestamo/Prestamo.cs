#region Header

// Creado por: Christian
// Fecha: 14/05/2018 02:34
// Actualizado por ultima vez: 01/06/2018 13:24

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Comun;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos.Prestamo
{
    public class Prestamo : Entity
    {
        #region Static

        // ReSharper disable once InconsistentNaming
        protected const int DIAS_POR_ANIO = 360;

        // ReSharper disable once InconsistentNaming
        protected const int DIAS_POR_MES = 30;

        #endregion

        #region Fields

        #endregion

        #region Properties

        public AnalistaFinanciero AnalistaFinanciero {get; protected set;}

        public string Codigo {get; protected set;}

        public decimal CostoEnvioACasa => this.ReglasDelPrestamoService.CostoEnvioACasa;

        public string NombreDelPrestamo => this.ReglasDelPrestamoService.NombreDelPrestamo;
        
        public string NombreDelBien => this.ReglasDelPrestamoService.NombreDelBien;
        
        public IEnumerable<CuotaSoloLectura> Cuotas
        {
            get
            {
                this.GenerarCronograma();
                return this.CuotasInternas.Select(c => new CuotaSoloLectura(c));
            }
        }

        public IEnumerable<CuotaSoloLectura> CuotasPendientes
        {
            get
            {
                this.GenerarCronograma();
                return this.CuotasInternas.Where(c => !c.EstaPagada()).Select(c => new CuotaSoloLectura(c));
            }
        }

        protected List<Cuota> CuotasInternas {get; set;}

        private void GenerarCronograma()
        {
            if(this.CuotasInternas.Count == 0)
            {
                this.CuotasInternas.AddRange(this.ReglasDelPrestamoService.GenerarCuotas(this.Importe,
                                                                                         this.NumeroCuotas,
                                                                                         this.DiasPorAnio,
                                                                                         this.DiasPorMes));
            }
        }

        public DescriptorPrestamo DescriptorPrestamo
        {
            get
            {
                this.GenerarCronograma();
                return new DescriptorPrestamo(this.ReglasDelPrestamoService.NombreDelPrestamo,
                                              this.Tea,
                                              this.NumeroCuotas,
                                              this.DiasPorAnio,
                                              this.DiasPorMes,
                                              this.Importe,
                                              this.Codigo,
                                              this.FechaDesembolso,
                                              this.DiaDePago,
                                              this.Prestatario,
                                              this.AnalistaFinanciero,
                                              this.Cuotas,
                                              this.TipoPrestamo,
                                              this.EsValido,
                                              this.ValorDelBien.HasValue,
                                              this.ReglasDelPrestamoService.NombreDelBien,
                                              this.ValorDelBien);
            }
        }

        public DescriptorPrestamo DescriptorPrestamoCuotasPendientes
        {
            get
            {
                this.GenerarCronograma();
                return new DescriptorPrestamo(this.ReglasDelPrestamoService.NombreDelPrestamo,
                                              this.Tea,
                                              this.NumeroCuotas,
                                              this.DiasPorAnio,
                                              this.DiasPorMes,
                                              this.Importe,
                                              this.Codigo,
                                              this.FechaDesembolso,
                                              this.DiaDePago,
                                              this.Prestatario,
                                              this.AnalistaFinanciero,
                                              this.CuotasPendientes,
                                              this.TipoPrestamo,
                                              this.EsValido,
                                              this.ValorDelBien.HasValue,
                                              this.ReglasDelPrestamoService.NombreDelBien,
                                              this.ValorDelBien);
            }
        }

        public int DiaDePago {get; protected set;}

        public int DiasPorAnio {get; protected set;}

        public int DiasPorMes {get; protected set;}

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public bool EnvioACasa {get; protected set;}

        public bool EsValido => this.EvaluarCemCliente();

        public DateTime FechaDesembolso {get; protected set;}

        public decimal Importe {get; protected set;}

        public decimal InteresMoratorio => this.ReglasDelPrestamoService.InteresMoratorio;

        public int NumeroCuotas {get; protected set;}

        public Prestatario.Prestatario Prestatario {get; protected set;}

        public decimal? TasaSeguroDelBien => this.ReglasDelPrestamoService.TasaSeguroDelBien;

        public decimal Tdes => this.ReglasDelPrestamoService.TasaSeguroDesgravamen;

        public decimal Tea => this.ReglasDelPrestamoService.TasaEfectivaAnual;

        public TipoPrestamo TipoPrestamo => this.ReglasDelPrestamoService.TipoPrestamo;

        public decimal? ValorDelBien {get; protected set;}

        #endregion

        #region Constructors

        protected IReglasDelPrestamoService ReglasDelPrestamoService;

        public Prestamo(IReglasDelPrestamoService reglasDelPrestamoService,
                        decimal importe,
                        int numeroCuotas,
                        DateTime fechaDesembolso,
                        int diaDePago,
                        Prestatario.Prestatario prestatario,
                        AnalistaFinanciero analistaFinanciero) : this()
        {   
            if(prestatario == null)
            {
                throw new Exception("Es necesario un prestatario.");
            }

            if(analistaFinanciero == null)
            {
                throw new Exception("Es necesario un analista financiero.");
            }

            if(reglasDelPrestamoService == null)
            {
                throw new Exception("Es necesario un conjunto de reglas para el prestamo.");
            }
            
            this.ReglasDelPrestamoService = reglasDelPrestamoService;

            var resultadoEvaluacion = this.ReglasDelPrestamoService.EvaluarPrestamo(prestatario, importe, numeroCuotas);
            
            if(!resultadoEvaluacion.Item1)
            {
                throw new Exception("No se cumplen con los requisitos del prestamo: " + string.Join(", ", resultadoEvaluacion.Item2));
            }
            
            this.EnvioACasa = false;
            this.DiasPorAnio = DIAS_POR_ANIO;
            this.DiasPorMes = DIAS_POR_MES;
            this.Importe = importe;
            this.NumeroCuotas = numeroCuotas;
            this.FechaDesembolso = fechaDesembolso;
            this.DiaDePago = diaDePago;
            this.Prestatario = prestatario;
            this.AnalistaFinanciero = analistaFinanciero;
            this.Codigo = Guid.NewGuid().ToString();

            this.GenerarCronograma();
        }

        /// <summary>
        ///     <remarks>Solo para uso interno.</remarks>
        /// </summary>
        protected Prestamo()
        {
            this.CuotasInternas = new List<Cuota>();
        }

        #endregion

        #region Methods

        private bool EvaluarCemCliente()
        {
            return this.ObtenerCuotaFijaMensual() < this.Prestatario.ObtenerCem();
        }

        public CuotaSoloLectura ObtenerCuota(int numeroCuota)
        {
            var cuota = this.CuotasInternas.Find(c => c.NumeroCuota == numeroCuota);
            if(cuota != null)
            {
                return new CuotaSoloLectura(cuota);
            }

            throw new Exception("No existe una cuota con ese numero.");
        }

        public decimal ObtenerCuotaFijaMensual()
        {
            return this.ReglasDelPrestamoService.ObtenerMontoCuotaFijaMensual(this.Importe,
                                                                              this.NumeroCuotas,
                                                                              this.DiasPorAnio,
                                                                              this.DiasPorMes);
        }

        public void PagarCuota(int numeroCuota, Cajero cajero)
        {
            var cuota = this.CuotasInternas.Find(c => c.NumeroCuota == numeroCuota);
            if(cuota != null)
            {
                cuota.Pagar(cajero);
            }
            else
            {
                throw new Exception("No existe una cuota con ese numero.");
            }
        }

        public bool TieneCuotaPorPagar()
        {
            return this.CuotasInternas.Any(cuota => !cuota.EstaPagada() && cuota.EstaVencida());
        }

        #endregion
    }
}