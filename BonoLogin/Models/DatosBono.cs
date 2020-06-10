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
        public string Metodo { get; set; }
        [Required]
        public double ValNominal { get; set; }
        [Required]
        public double ValComercial { get; set; }
        [Required]
        public int AnosVida { get; set; }
        [Required]
        public string TipoAno { get; set; }
        [Required]
        public string FrecPago { get; set; }
        [Required]
        public double Tea { get; set; }
        [Required]
        public double Tdea { get; set; }
        [Required]
        public double Ianual { get; set; }
        [Required]
        public double Ir { get; set; }
        [Required]
        public double Pprima { get; set; }
        [Required]
        public double Pestimacion { get; set; }
        [Required]
        public double Pcolocacion { get; set; }
        [Required]
        public double Pflotacion { get; set; }
        [Required]
        public double PCavali { get; set;}
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<PGracia> PGracia { get; set; }

        public virtual ResultadoBono ResultadoBono { get; set; }
    }
}