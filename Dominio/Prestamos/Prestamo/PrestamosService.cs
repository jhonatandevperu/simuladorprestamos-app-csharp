using System;
using System.Linq;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos.Prestamo
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PrestamosService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly PrestamoFactory _prestamoFactory;

        public PrestamosService(IPrestamoRepository prestamoRepository, PrestamoFactory prestamoFactory)
        {
            this._prestamoRepository = prestamoRepository;
            this._prestamoFactory = prestamoFactory;
        }

        private Prestamo CrearPrestamo(Prestatario.Prestatario prestatario,
                                       AnalistaFinanciero analistaFinanciero,
                                       TipoPrestamo tipoPrestamo,
                                       decimal importe,
                                       int numeroCuotas,
                                       DateTime fechaDesembolso,
                                       int diaDePago,
                                       bool envioACasa = false)
        {
            if(this.EvaluarDeudasPendientesDelPrestatario(prestatario))
            {
                return this._prestamoFactory.Build(tipoPrestamo,
                                            importe,
                                            numeroCuotas,
                                            fechaDesembolso,
                                            diaDePago,
                                            prestatario,
                                            analistaFinanciero);
            }
            else
            {
                throw new Exception("El prestatario no puede acceder a este préstamo.");
            }
        }

        public DescriptorPrestamo SolicitarPrestamo(Prestatario.Prestatario prestatario,
                                                    AnalistaFinanciero analistaFinanciero,
                                                    TipoPrestamo tipoPrestamo,
                                                    decimal importe,
                                                    int numeroCuotas,
                                                    DateTime fechaDesembolso,
                                                    int diaDePago,
                                                    bool envioACasa = false)
        {
            return this.CrearPrestamo(prestatario,
                                      analistaFinanciero,
                                      tipoPrestamo,
                                      importe,
                                      numeroCuotas,
                                      fechaDesembolso,
                                      diaDePago,
                                      envioACasa).DescriptorPrestamo;
        }

        public void RealizarPrestamo(Prestatario.Prestatario prestatario,
                                     AnalistaFinanciero analistaFinanciero,
                                     TipoPrestamo tipoPrestamo,
                                     decimal importe,
                                     int numeroCuotas,
                                     DateTime fechaDesembolso,
                                     int diaDePago,
                                     bool envioACasa = false)
        {
            var prestamo = this.CrearPrestamo(prestatario,
                                              analistaFinanciero,
                                              tipoPrestamo,
                                              importe,
                                              numeroCuotas,
                                              fechaDesembolso,
                                              diaDePago,
                                              envioACasa);

            if(prestamo.EsValido)
            {
                this._prestamoRepository.RegistrarPrestamo(prestamo);
            }
            else
            {
                throw new Exception("El cliente no puede acceder al prestamo.");
            }
        }

        public CuotaSoloLectura RealizarPago(Prestatario.Prestatario prestatario, Cajero cajero, string codigoPrestamo, int cuota, String comando = "solo_mostrar")
        {
            var prestamo =
                            this._prestamoRepository.RecuperarPrestamoDelPrestatarioDeCodigo(prestatario.Id, codigoPrestamo);
            if (prestamo == null)
            {
                throw new Exception("No existe un prestamo con ese codigo relacionado al prestatario.");
            }

            prestamo.PagarCuota(cuota, cajero);

            if (String.Equals(comando, "solo_mostrar"))
            {
                return prestamo.ObtenerCuota(cuota);
            }
            else if (String.Equals(comando, "guardar_bd"))
            {
                this._prestamoRepository.RegistrarPago(prestamo, cuota);
                return prestamo.ObtenerCuota(cuota);
            }
            else
            {
                throw new Exception("No se ha podido realizar el pago.");
            }

        }

        public DescriptorPrestamo ObtenerPrestamoConCuotasPendientes(Prestatario.Prestatario prestatario, string codigoPrestamo)
        {
            var prestamo = this._prestamoRepository.RecuperarPrestamoDelPrestatarioDeCodigo(prestatario.Id, codigoPrestamo);
            return prestamo.DescriptorPrestamoCuotasPendientes;
        }

        #region Validaciones

        private bool EvaluarDeudasPendientesDelPrestatario(Prestatario.Prestatario prestatario)
        {
            var cronogramas = this._prestamoRepository.PrestamosActivosDelPrestatario(prestatario.Id);
            return cronogramas.All(cronograma => !cronograma.TieneCuotaPorPagar());
        }

        #endregion
    }
}