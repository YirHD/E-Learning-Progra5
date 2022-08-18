using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Lecciones
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLeccion { get; set; }
        public string Titulo { get; set; }
        public String Contenido { get; set; }
        public CursoProfe CursoProfe { get; set; }
        public List<Archivo> Archivos { get; set; }
        public List<Comentarios> Comentarios { get; set; }

    }
}
