using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonoLogin.Models
{
    public class DatosEmisor
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

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
        public double PCavali { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}