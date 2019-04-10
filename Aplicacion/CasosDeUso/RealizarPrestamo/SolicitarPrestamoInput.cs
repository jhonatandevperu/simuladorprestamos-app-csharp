#region Header

// Creado por: Christian
// Fecha: 29/05/2018 08:32
// Actualizado por ultima vez: 29/05/2018 08:32

#endregion

using System;

namespace Aplicacion.CasosDeUso.RealizarPrestamo
{
    public class SolicitarPrestamoInput
    {
        public string NombreUsuario {get;}
        public string DNICliente {get;}
        public decimal Importe {get;}
        public int NumeroCuotas {get;}
        public DateTime FechaDesembolso {get;}
        public int DiaDePago {get;}
        public decimal? ValorDelBien {get;}

        public SolicitarPrestamoInput(string nombreUsuario,
                                      string dniCliente,
                                      decimal importe,
                                      int numeroCuotas,
                                      DateTime fechaDesembolso,
                                      decimal? valorDelBien = null)
        {
            this.NombreUsuario = nombreUsuario;
            this.DNICliente = dniCliente;
            this.Importe = importe;
            this.NumeroCuotas = numeroCuotas;
            this.FechaDesembolso = fechaDesembolso;
            this.DiaDePago = fechaDesembolso.Day;
            this.ValorDelBien = valorDelBien;
        }
    }
}