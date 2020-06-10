using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonoLogin.Models
{
    public class FlujoEmisorEsc
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Periodo { get; set; }
        [Required]
        public double Monto { get; set; }

        public int ResultadoBonoId { get; set; }
        public virtual ResultadoBono ResultadoBono { get; set; }
    }
}