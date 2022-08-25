using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    public class Matricula
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMatricula { get; set; }

        public ApplicationUser Alumno { get; set; }

        public CursoProfe CursoProfe { get; set; }
    }
}
