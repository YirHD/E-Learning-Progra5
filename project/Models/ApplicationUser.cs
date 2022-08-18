using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string nombre { get; set; }
        public int cedula { get; set; }
        public string apellidos { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaNacimiento { get; set; }
        public List<CursoProfe> CursoProfe { get; set; }
        public List<Matricula> Matriculas { get; set; }
    }
}
