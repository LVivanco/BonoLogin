using System.ComponentModel.DataAnnotations;

namespace BonoLogin.Models
{
    public class PGracia
    {
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Periodo { get; set; }
        public int DatosBonoId { get; set; }
        public virtual DatosBono DatosBono { get; set; }
    }
}