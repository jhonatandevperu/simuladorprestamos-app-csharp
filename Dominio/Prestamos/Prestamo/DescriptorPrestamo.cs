#region Header

// Creado por: Christian
// Fecha: 22/05/2018 09:13
// Actualizado por ultima vez: 22/05/2018 09:13

#endregion

using System;
using System.Collections.Generic;
using Dominio.Prestamos.Trabajadores;

namespace Dominio.Prestamos.Prestamo
{
    public class DescriptorPrestamo
    {
        public DescriptorPrestamo(string nombreDelPrestamo,
                                  decimal tea,
                                  int numeroCuotas,
                                  int diasPorAnio,
                                  int diasPorMes,
                                  decimal importe,
                                  string codigo,
                                  DateTime fechaDesembolso,
                                  int diaDePago,
                                  Prestatario.Prestatario prestatario,
                                  AnalistaFinanciero analistaFinanciero,
                                  IEnumerable<CuotaSoloLectura> cronograma,
                                  TipoPrestamo tipoPrestamo,
                                  bool esValido,
                                  bool estaRelacionadoAUnBien,
                                  string nombreDelBien,
                                  decimal? valorDelBien)
        {
            this.NombreDelBien = nombreDelBien;
            this.TipoPrestamo = nombreDelPrestamo;
            this.ValorDelBien = valorDelBien;
            this.Prestatario = prestatario;
            this.AnalistaFinanciero = analistaFinanciero;
            this.Cronograma = cronograma;
            this.EsValido = esValido;
            this.Tea = tea;
            this.NumeroCuotas = numeroCuotas;
            this.DiasPorAnio = diasPorAnio;
            this.DiasPorMes = diasPorMes;
            this.Importe = importe;
            this.Codigo = codigo;
            this.FechaDesembolso = fechaDesembolso;
            this.DiaDePago = diaDePago;
            this.EstaRelacionadoAUnBien = estaRelacionadoAUnBien;
        }

        public Prestatario.Prestatario Prestatario {get;}
        public AnalistaFinanciero AnalistaFinanciero {get;}
        public IEnumerable<CuotaSoloLectura> Cronograma {get;}
        public bool EsValido {get;}
        public decimal Tea {get;}
        public int NumeroCuotas {get;}
        public int DiasPorAnio {get;}
        public int DiasPorMes {get;}
        public decimal Importe {get;}
        public string Codigo {get;}
        public DateTime FechaDesembolso {get;}
        public int DiaDePago {get;}
        public string TipoPrestamo {get;}
        public bool EstaRelacionadoAUnBien {get;}
        public string NombreDelBien {get;}
        public decimal? ValorDelBien {get;}
    }
}