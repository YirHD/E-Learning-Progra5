using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project.Models;

namespace project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<CursoProfe> CursoProfes { get; set; }
        public DbSet<Curso> Cursoes { get; set; }

        public DbSet<Matricula> Matriculas { get; set; }

        public DbSet<Lecciones> Lecciones { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
    }
}