using System.ComponentModel.DataAnnotations;
namespace API.P.Movies.DAL.Models
{
    public class AuditBase
    {
        //Auditar todas las fechas de transacciones en la base de datos. Registrar esa fecha de creacion en la base de datos
        [Key]
        public virtual int Id { get; set; } //Primary Key
        public virtual DateTime CreatedDate { get; set; } //Fecha de creacion
        public virtual DateTime? UpdateDate { get; set; } //Fecha de actualizacion


    }
}
