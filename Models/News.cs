using System.ComponentModel.DataAnnotations;

namespace PortalKibica.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(200)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Treść jest wymagana")]
        [Display(Name = "Treść")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Zdjęcie")]
        public string? ImagePath { get; set; }

        [Display(Name = "Data publikacji")]
        public DateTime PublishDate { get; set; } = DateTime.Now;
    }
}