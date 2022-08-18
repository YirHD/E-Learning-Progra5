using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Comentarios
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComentario { get; set; }
        public string Comentario { get; set; }
        public Lecciones Leccion { get; set; }
        public ApplicationUser Usuario { get; set; }

    }
}
