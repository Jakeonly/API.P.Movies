using System.ComponentModel.DataAnnotations;
namespace API.P.Movies.DAL.Models
{
    public class Category : AuditBase
    {
        [Required] //Decorator tambien se llama Data annotations
        [Display(Name = "Categoria")] //Este decorator es para mostrar un nombre amigable en la interfaz de usuario
        public String Name { get; set; }

    }
}