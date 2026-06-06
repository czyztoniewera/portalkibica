using System.ComponentModel.DataAnnotations;

namespace PortalKibica.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane")]
        [StringLength(100)]
        [Display(Name = "Imię i nazwisko")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [StringLength(50)]
        [Display(Name = "Pozycja")]
        public string Position { get; set; } = string.Empty;

        [Display(Name = "Numer")]
        public int Number { get; set; }

        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Display(Name = "Zdjęcie")]
        public string? ImagePath { get; set; }
    }
}