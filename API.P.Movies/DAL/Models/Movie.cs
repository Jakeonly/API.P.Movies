using System.ComponentModel.DataAnnotations;
namespace API.P.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        [Display(Name = "Pelicula")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Duración")]
        public int Duration { get; set; }
        [Display(Name = "Descripción")]
        public String Description { get; set; } //Campo debe aceptar nulls
        [Required]
        [Display(Name = "Clasificación")]
        public String Clasification { get; set; }
    }
}