using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class Cliente
    {
        [Required]
        [DisplayName("DNI")]
        [StringLength(8)]
        public string Dni {get; set;}
        
        [Required]
        [DisplayName("Nombres")]
        public string Nombres {get; set;}
        
        [Required]
        [DisplayName("Apellidos")]
        public string Apellidos {get; set;}
        
        [Required]
        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento {get; set;}
        
        [Required]
        [DisplayName("Estado Civil")]
        public bool EstadoCivil {get; set;}
        
        [Required]
        [DisplayName("N° Hijos")]
        public int NumeroDeHijos {get; set;}
        
        [Required]
        [DisplayName("Celular")]
        public string NumeroDeCelular {get; set;}
        
        [Required]
        [DisplayName("Operador")]
        public string Operador {get; set;}
        
        [Required]
        [DisplayName("Ingresos Totales Mensuales")]
        public double IngresosTotalesMensuales {get; set;}
        
        [Required]
        [DisplayName("Gastos Fijos Mensuales")]
        public double GastosFijosMensuales {get; set;}
        
        [Required]
        [DisplayName("Departamento")]
        public string Departamento {get; set;}
        
        [Required]
        [DisplayName("Ciudad")]
        public string Ciudad {get; set;}
    }
}