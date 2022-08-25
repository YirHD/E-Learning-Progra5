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
    public class GruposAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GruposAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GruposAdmin
        public async Task<IActionResult> Index(string searchProfe, string searchCurso)
        {
            string s = User.Identity.Name;
            ApplicationUser usuario = (ApplicationUser)_context.Users.Where(x => x.Email == s).SingleOrDefault();

            var grupos = _context.CursoProfes.Include(x => x.Curso).Include(x => x.Matricula).Include(x => x.Profe).ToList();

            if (User.IsInRole("Profe"))
            {
                grupos = grupos.Where(x => x.Profe.Id == usuario.Id).ToList();
            }

            if (User.IsInRole("Alumno"))
            {
                var matriculas = _context.Matriculas.Where(x => x.Alumno.Id == usuario.Id).ToList();
                List<CursoProfe> cursosMat = new List<CursoProfe>();

                foreach (var mat in matriculas)
                {
                    CursoProfe grupo = new CursoProfe();
                    grupo = _context.CursoProfes.Include(x => x.Curso).Include(x => x.Profe).Include(x => x.Lecciones).Where(x => x.IdCursoProfe == mat.CursoProfe.IdCursoProfe).SingleOrDefault();
                    cursosMat.Add(grupo);
                }

                grupos = cursosMat;

            }


            if (!String.IsNullOrEmpty(searchProfe))
            {
                grupos = grupos.Where(g => g.Profe.nombre.Contains(searchProfe)).ToList();
            }

            if (!String.IsNullOrEmpty(searchCurso))
            {
                grupos = grupos.Where(g => g.Curso.NombreCurso.Contains(searchCurso)).ToList();
            }

            return View(grupos);
        }

        // GET: GruposAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CursoProfe cursoProfe = _context.CursoProfes.Include(x => x.Curso).Include(x => x.Profe).SingleOrDefault(x => x.IdCursoProfe == id);
            List<Lecciones> lecciones = _context.Lecciones.Where(x => x.CursoProfe.IdCursoProfe == cursoProfe.IdCursoProfe).ToList();
            ViewBag.cantlecc = lecciones.Count;
            ViewBag.Lecciones = lecciones;

            List<Matricula> matriculas = _context.Matriculas.Include(x => x.Alumno).Include(x => x.CursoProfe).Where(x => x.CursoProfe.IdCursoProfe == cursoProfe.IdCursoProfe).ToList();
            List<ApplicationUser> alumnos = new List<ApplicationUser>();

            if (matriculas.Count > 0)
            {
                foreach (Matricula mat in matriculas)
                {
                    alumnos.Add(mat.Alumno);
                }
            }

            ViewBag.alumnos = alumnos;

            if (cursoProfe == null)
            {
                return BadRequest();
            }
            return View(cursoProfe);
        }

        // GET: GruposAdmin/Create
        
        // GET: GruposAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            CursoProfe cursoProfe = _context.CursoProfes.Find(id);
            if (cursoProfe == null)
            {
                return NotFound();
            }
            return View(cursoProfe);
        }

        // POST: GruposAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCursoProfe")] CursoProfe cursoProfe)
        {
            if (id != cursoProfe.IdCursoProfe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursoProfe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoProfeExists(cursoProfe.IdCursoProfe))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cursoProfe);
        }

        // GET: GruposAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CursoProfes == null)
            {
                return NotFound();
            }

            var cursoProfe = await _context.CursoProfes
                .FirstOrDefaultAsync(m => m.IdCursoProfe == id);
            if (cursoProfe == null)
            {
                return NotFound();
            }

            return View(cursoProfe);
        }

        // POST: GruposAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CursoProfes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CursoProfes'  is null.");
            }
            var cursoProfe = await _context.CursoProfes.FindAsync(id);
            if (cursoProfe != null)
            {
                _context.CursoProfes.Remove(cursoProfe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoProfeExists(int id)
        {
          return (_context.CursoProfes?.Any(e => e.IdCursoProfe == id)).GetValueOrDefault();
        }
    }
}
