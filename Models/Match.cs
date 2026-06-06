using System.ComponentModel.DataAnnotations;

namespace PortalKibica.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rywal jest wymagany")]
        [StringLength(100)]
        [Display(Name = "Rywal")]
        public string Opponent { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data meczu jest wymagana")]
        [Display(Name = "Data meczu")]
        public DateTime MatchDate { get; set; }

        [Required(ErrorMessage = "Stadion jest wymagany")]
        [StringLength(100)]
        [Display(Name = "Stadion")]
        public string Stadium { get; set; } = string.Empty;

        [Display(Name = "Wynik")]
        public string? Result { get; set; }
    }
}