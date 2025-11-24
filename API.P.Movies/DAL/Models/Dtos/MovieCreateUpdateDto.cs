using System.ComponentModel.DataAnnotations;

namespace API.P.Movies.DAL.Models.Dtos
{
    public class MovieCreateUpdateDto
    {
        //=======================================================================================================//
        [Required(ErrorMessage = "El nombre de la pelicula es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre de la pelicula no puede exceder los 100 caracteres")]
        public String Name { get; set; }
        //=======================================================================================================//
        [Required(ErrorMessage = "La duración de la pelicula es obligatoria")]
        public int Duration { get; set; }
        //=======================================================================================================//
        [MaxLength(500, ErrorMessage = "La descripción de la pelicula no puede exceder los 500 caracteres")]
        public String Description { get; set; }
        //=======================================================================================================//
        [Required(ErrorMessage = "La clasificación de la pelicula es obligatoria")]
        [MaxLength(50, ErrorMessage = "La descripción de la pelicula no puede exceder los 50 caracteres")]
        public String Clasification { get; set; }
        //=======================================================================================================//
    }
}
