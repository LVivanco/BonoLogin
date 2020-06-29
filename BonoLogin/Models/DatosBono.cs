using BonoLogin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonoLogin.Models
{
    public class DatosBono
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Tipo de Moneda")]
        public string Moneda { get; set; }
        [Required]
        [Display(Name = "Metodo")]
        public string Metodo { get; set; }
        [Required]
        [Display(Name = "Valor Nominal")]
        public double ValNominal { get; set; }
        [Required]
        [Display(Name = "Valor Comercial")]
        public double ValComercial { get; set; }
        [Required]
        [Display(Name = "Años de vida")]
        public int AnosVida { get; set; }
        [Required]
        [Display(Name = "Tipo de año")]
        public string TipoAno { get; set; }
        [Required]
        [Display(Name = "Frecuencia de pago")]
        public string FrecPago { get; set; }
        [Required]
        [Display(Name = "Tasa efectiva anual %")]
        public double Tea { get; set; }
        [Required]
        [Display(Name = "Tasa de descuento efectiva anual %")]
        public double Tdea { get; set; }
        [Required]
        [Display(Name = "Inflación anual %")]
        public double Ianual { get; set; }
        [Required]
        [Display(Name = "Impuesto a la Renta %")]
        public double Ir { get; set; }
        [Required]
        [Display(Name = "Prima %")]
        public double Pprima { get; set; }
        [Required]
        [Display(Name = "Estructuración %")]
        public double Pestructuracion { get; set; }
        [Required]
        [Display(Name = "Colocación %")]
        public double Pcolocacion { get; set; }
        [Required]
        [Display(Name = "Flotación %")]
        public double Pflotacion { get; set; }
        [Required]
        [Display(Name = "Cavali %")]
        public double PCavali { get; set;}
        
        [Required]
        [Display(Name = "Fecha de emisión")]
        public DateTime fechaEmision { get; set;}
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Display(Name = "Plazos de gracia")]
        public virtual ICollection<PGracia> PGracia { get; set; }

    }
}