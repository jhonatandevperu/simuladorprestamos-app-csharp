using System;
using Dominio.Comun;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos
{
    public class Cuota
    {
        protected Cuota()
        {
        }

        internal Cuota(decimal interesMoratorio,
                       int numeroCuota,
                       DateTime fechaVencimiento,
                       decimal saldoInicial,
                       decimal amortizacion,
                       decimal interesMensual,
                       decimal seguroDesgravamenMensual,
                       decimal cuotaFija,
                       decimal? seguroDelBien = null)
        {
            this.InteresMoratorio = interesMoratorio;
            this.NumeroCuota = numeroCuota;
            this.FechaVencimiento = fechaVencimiento;
            this.SaldoInicial = Math.Round(saldoInicial, 2);
            this.Amortizacion = Math.Round(amortizacion, 2);
            this.InteresMensual = Math.Round(interesMensual, 2);
            this.SeguroDesgravamenMensual = Math.Round(seguroDesgravamenMensual, 2);
            this.CuotaFija = Math.Round(cuotaFija, 2);
            if(seguroDelBien.HasValue)
            {
                this.SeguroDelBien = Math.Round(seguroDelBien.Value, 2);
            }
        }

        public decimal ObtenerPagoTotal()
        {
            return this.CuotaFija + this.CalcularMora();
        }

        public void Pagar(Cajero cajero)
        {
            if(this.EstaPagada())
            {
                throw new Exception("La cuota ya esta pagada.");
            }
            
            this.Pago = new Pago(cajero, DateTime.Now.Date, this.CuotaFija, this.CalcularMora());
        }

        public bool EstaPagada()
        {
            return this.Pago != null;
        }

        public decimal CalcularMora()
        {
            if(this.EstaVencida())
            {
                var diasDeMora = DateTime.Now.Date.DiffInDays(this.FechaVencimiento.Date);
                var mora = this.CuotaFija * this.InteresMoratorio * diasDeMora;
                if(mora < 50)
                {
                    mora = 50;
                }
                return mora;                
            }
            return 0;
        }

        public bool EstaVencida()
        {
            if(this.EstaPagada())
            {
                return this.FechaVencimiento.Date < this.Pago.Fecha;
            }
            else
            {
                return this.FechaVencimiento.Date < DateTime.Now.Date;                
            }
        }

        public int NumeroCuota {get; protected set;}
        public DateTime FechaVencimiento {get; protected set;}
        public decimal SaldoInicial {get; protected set;}
        public decimal Amortizacion {get; protected set;}
        public decimal InteresMensual {get; protected set;}
        public decimal SeguroDesgravamenMensual {get; protected set;}
        public decimal CuotaFija {get; protected set;}
        public decimal? SeguroDelBien {get; protected set;}

        public Pago Pago {get; protected set;}

        public decimal InteresMoratorio {get; protected set;}
    }
}