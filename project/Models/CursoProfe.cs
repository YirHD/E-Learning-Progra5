﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project.Models
{
    [Table("TB_CursoProfe")]
    public class CursoProfe
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCursoProfe { get; set; }
        [Required]
        public ApplicationUser Profe { get; set; }
        [Required]
        public Curso Curso { get; set; }

        public List<Matricula> Matricula { get; set; }
        public List<Lecciones> Lecciones { get; set; }

    }
}
