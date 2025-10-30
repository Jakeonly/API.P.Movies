using System.ComponentModel.DataAnnotations;

namespace API.P.Movies.DAL.Models.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage ="El nombre de la categoria es obligatorio")]
        [MaxLength(100, ErrorMessage ="El nombre de la categoria no puede exceder los 100 caracteres")]
        public string Name { get; set; }
    }
}
