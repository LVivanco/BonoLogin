using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonoLogin.Models
{
    public class ResultadoBono
    {
        public int Id { get; set; }
        [Required]
        public double Van { get; set; }
        [Required] 
        public double Va { get; set; }
        [Required] 
        public double Tcea { get; set; }
        [Required] 
        public double Tir { get; set; }
        
        public virtual ICollection<FlujoEmisor> FlujoEmisor { get; set; }
        public virtual ICollection<FlujoEmisorEsc> FlujoEmisorEsc { get; set; }
        public virtual ICollection<FlujoBonista> FlujoBonista { get; set; }
    }
}