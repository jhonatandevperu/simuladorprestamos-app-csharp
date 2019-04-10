using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dominio.Prestamos.Prestamo;

namespace Aplicacion.CasosDeUso.RealizarPrestamo
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class DescriptorPrestamoOutput
    {
        public string TipoPrestamo {get;}
        public bool EstaRelacionadoAUnBien {get;}
        public string NombreDelBien {get;}
        public string NombreAnalista {get;}
        public string NombrePrestatario {get;}
        public decimal CemPrestatario {get;}
        public string CodigoPrestamo {get;}
        public decimal Tea {get;}
        public decimal Importe {get;}
        public int NumeroCuotas {get;}
        public int DiasPorAnio {get;}
        public int DiasPorMes {get;}
        public DateTime FechaDesembolso {get;}
        public int DiaDePago {get;}
        public IEnumerable<DescriptorCuota> DescriptorCronograma {get;}
        public bool EsValido {get;}

        internal DescriptorPrestamoOutput(DescriptorPrestamo descriptorDominio)
        {
            this.NombreAnalista = descriptorDominio.AnalistaFinanciero.NombreCompleto;
            this.NombrePrestatario = descriptorDominio.Prestatario.NombreCompleto;
            this.CemPrestatario = descriptorDominio.Prestatario.ObtenerCem();
            this.CodigoPrestamo = descriptorDominio.Codigo;
            this.Tea = descriptorDominio.Tea;
            this.Importe = descriptorDominio.Importe;
            this.NumeroCuotas = descriptorDominio.NumeroCuotas;
            this.DiasPorAnio = descriptorDominio.DiasPorAnio;
            this.DiasPorMes = descriptorDominio.DiasPorMes;
            this.FechaDesembolso = descriptorDominio.FechaDesembolso;
            this.DiaDePago = descriptorDominio.DiaDePago;
            this.EsValido = descriptorDominio.EsValido;
            this.TipoPrestamo = descriptorDominio.TipoPrestamo;
            this.EstaRelacionadoAUnBien = descriptorDominio.EstaRelacionadoAUnBien;
            this.NombreDelBien = descriptorDominio.NombreDelBien;
            this.DescriptorCronograma = descriptorDominio.Cronograma.Select(cuota => new DescriptorCuota(cuota));
        }
    }
}