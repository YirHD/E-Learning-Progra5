using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class LeccionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lecciones
      
        // GET: Lecciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lecciones lec = _context.Lecciones.Include(x => x.CursoProfe).SingleOrDefault(x => x.IdLeccion == id);
            var Docente = _context.CursoProfes.Include(x => x.Profe).SingleOrDefault(x => x.IdCursoProfe == lec.CursoProfe.IdCursoProfe).Profe;
            var Recursos = _context.Archivos.Where(x => x.lecciones.IdLeccion == id).ToList();


            //Envio de id usuario:

            string s = User.Identity.Name;
            String idusuario = _context.Users.Where(x => x.Email == s).SingleOrDefault().Id;

            List<Comentarios> coments = _context.Comentarios.Include(x => x.Leccion).Include(x => x.Usuario).Where(x => x.Leccion.IdLeccion == lec.IdLeccion).ToList();

            ViewBag.cantcoments = coments.Count;
            ViewBag.comentarios = coments;
            ViewBag.idUsuario = idusuario;
            ViewBag.recursos = Recursos;
            ViewBag.docente = "" + Docente.nombre + " " + Docente.apellidos;
            if (lec == null)
            {
                return NotFound();
            }

            return View(lec);
        }

        // GET: Lecciones/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ViewBag.idCursoP = id;
            Lecciones lec = new Lecciones();
            lec.CursoProfe = _context.CursoProfes.Find(id);

            return View(lec);
        }

        // POST: Lecciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecciones leccion, int? Grupo)
        {
            try
            {
              

                var grupo = _context.CursoProfes.Include(x => x.Profe).Include(x => x.Curso).SingleOrDefault(x => x.IdCursoProfe == Grupo);
                leccion.CursoProfe = grupo;

                _context.Lecciones.Add(leccion);
                _context.SaveChanges();

                return RedirectToAction("Details", "GruposAdmin", new { id = grupo.IdCursoProfe });
            }
            catch
            {
                return View();
            }
        }

        // GET: Lecciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Lecciones leccion = _context.Lecciones.Include(x => x.CursoProfe).SingleOrDefault(x => x.IdLeccion == id);
            return View(leccion);
        }

        // POST: Lecciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLeccion,Titulo,Contenido")] Lecciones lec)
        {
            try
            {
              

                var leccion = _context.Lecciones.Include(x => x.CursoProfe).SingleOrDefault(x => x.IdLeccion == id);

                leccion.Contenido = lec.Contenido;
                leccion.Titulo = lec.Titulo;

                _context.Entry(leccion).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Details", "GruposAdmin", new { id = leccion.CursoProfe.IdCursoProfe });
            }
            catch
            {
                return View();
            }
        }

        // GET: Lecciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Lecciones lec = _context.Lecciones.Find(id);
            if (lec == null)
            {
                return NotFound();
            }
            return View(lec);
        }

        // POST: Lecciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Lecciones lec = _context.Lecciones.Include(x => x.CursoProfe).Include(x => x.Comentarios).Include(x => x.Archivos).SingleOrDefault(x => x.IdLeccion == id);
                int idGrupo = lec.CursoProfe.IdCursoProfe;
                _context.Lecciones.Remove(lec);
                _context.SaveChanges();
                return RedirectToAction("Details", "GruposAdmin", new { id = idGrupo });
            }
            catch (Exception e)
            {
                Lecciones lec = _context.Lecciones.Find(id);
                return View(lec);
            }
        }
    }
}
