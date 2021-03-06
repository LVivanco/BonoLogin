﻿using System.ComponentModel.DataAnnotations;

namespace BonoLogin.Models
{
    public class ResultadoBono
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Valor actual neto")]
        public double Van { get; set; }
        [Required]
        [Display(Name = "Valor actual")]
        public double Va { get; set; }
        [Required]
        [Display(Name = "TREA Bonista %")]
        public double TceaBonista { get; set; }
        [Required]
        [Display(Name = "TIR Bonista %")]
        public double TirBonista { get; set; }
        [Required]
        [Display(Name = "TCEA Emisor %")]
        public double TceaEmisor { get; set; }
        [Required]
        [Display(Name = "TIR Emisor %")]
        public double TirEmisor { get; set; }

        [Display(Name = "Bono")]
        public string Bono { get; set; }

        [Display(Name = "Bono Indexado")]
        public string BonoIndexado { get; set; }

        [Display(Name = "Cupon")]
        public string Cupon { get; set; }

        [Display(Name = "Cuota")]
        public string Cuota { get; set; }

        [Display(Name = "Amortización")]
        public string Amortizacion { get; set; }

        [Display(Name = "Prima")]
        public string Prima { get; set; }

        [Display(Name = "Escudo")]
        public string Escudo { get; set; }

        [Display(Name = "Flujo Emisor")]
        public string FlujoEmisor { get; set; }
        [Display(Name = "Flujo Emisor Escudo")]
        public string FlujoEmisorEsc { get; set; }
        [Display(Name = "Flujo Bonista")]
        public string FlujoBonista { get; set; }

        [Display(Name = "Fechas")]
        public string FlujoFechas { get; set; }

        [Display(Name = "Inflación por periodo")]
        public double Ip { get; set; }

        [Display(Name = "Tasa del Periodo")]
        public double Tep { get; set; }

        [Display(Name = "Cok del Periodo")]
        public double Tdep { get; set; }
        public int DatosBonoId { get; set; }
        public virtual DatosBono DatosBono { get; set; }

    }
}