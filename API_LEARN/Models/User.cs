using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_LEARN.Models
{
    [Table("users")]
    public class User()
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } // Asegúrate de que no haya errores de nombre aquí
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50)]
        public string name { get; set; }
        public string email { get; set; }
    }
}
