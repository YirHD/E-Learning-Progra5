using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext db;

       
        public CursosController(ApplicationDbContext context,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.db = context;
        }

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        //// GET: Cursos
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Cursoes != null ? 
        //                  View(await _context.Cursoes.ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.Cursoes'  is null.");
        //}

        //// GET: Cursos/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Cursoes == null)
        //    {
        //        return NotFound();
        //    }

        //    var curso = await _context.Cursoes
        //        .FirstOrDefaultAsync(m => m.IdCurso == id);
        //    if (curso == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(curso);
        //}

        //// GET: Cursos/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        //// GET: Cursos/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Cursoes == null)
        //    {
        //        return NotFound();
        //    }

        //    var curso = await _context.Cursoes.FindAsync(id);
        //    if (curso == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(curso);
        //}

        //// POST: Cursos/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdCurso,CodigoCurso,NombreCurso,DescripcionCurso")] Curso curso)
        //{
        //    if (id != curso.IdCurso)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(curso);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CursoExists(curso.IdCurso))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(curso);
        //}

        //// GET: Cursos/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Cursoes == null)
        //    {
        //        return NotFound();
        //    }

        //    var curso = await _context.Cursoes
        //        .FirstOrDefaultAsync(m => m.IdCurso == id);
        //    if (curso == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(curso);
        //}

        //// POST: Cursos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Cursoes == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Cursoes'  is null.");
        //    }
        //    var curso = await _context.Cursoes.FindAsync(id);
        //    if (curso != null)
        //    {
        //        _context.Cursoes.Remove(curso);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CursoExists(int id)
        //{
        //  return (_context.Cursoes?.Any(e => e.IdCurso == id)).GetValueOrDefault();
        //}


        // GET: Cursos
        public ActionResult Index()
        {
            return View(db.Cursoes.ToList());
        }

        // GET: Cursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Curso curso = db.Cursoes.Find(id);
            if (curso == null)
            {
                return BadRequest();
            }
            return View(curso);
        }

        // GET: Cursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCurso,CodigoCurso,NombreCurso,DescripcionCurso")] Curso curso)
        {
          
                db.Add(curso);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
    

        }


        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Curso curso = db.Cursoes.Find(id);
            if (curso == null)
            {
                return BadRequest();
            }
            return View(curso);
        }

        // POST: Cursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Curso curso)
        {
            
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
       
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Curso curso = db.Cursoes.Find(id);
            if (curso == null)
            {
                return BadRequest();
            }
            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursoes.Find(id);
            db.Cursoes.Remove(curso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        //Acción para generar curso
        //Get Cursos/Grupo

        public async Task<ActionResult> Grupo(int? id, string SearchString)
        {

            if (id == null)
            {
                return BadRequest();
            }

            Curso curso = db.Cursoes.Find(id);

            if (curso == null)
            {
                return BadRequest();
            }

            //Lista de profesores
            var role = await _roleManager.FindByNameAsync("Profe");

            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add((ApplicationUser)user);
                }
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(x => x.nombre.Contains(SearchString)).ToList();

            }

            ViewBag.profes = users;
            ViewBag.curso = curso;

            return View(curso);
        }

        //Acción para generar curso
        //Get Cursos/GeneraGrupo
        public ActionResult GeneraGrupo(string idProfe, int? idCurso)
        {
            if (String.IsNullOrEmpty(idProfe) || idCurso == null)
            {
                return BadRequest();
            }

            var profe = db.Users.Find(idProfe);
            var curso = db.Cursoes.Find(idCurso);

            if (profe == null || curso == null)
            {
                return BadRequest();
            }

            CursoProfe grupo = new CursoProfe();
            grupo.Curso = curso;
            grupo.Profe = (ApplicationUser)profe;

            db.CursoProfes.Add(grupo);
            db.SaveChanges();



            return RedirectToAction("Index", "GruposAdmin");
        }

        //Acción para matricula en curso a un estudiante
        // GET

        public async Task<ActionResult> Matricula(int? id, string SearchString)
        {

            if (id == null)
            {
                return BadRequest();
            }

            Curso curso = db.Cursoes.Find(id);

            if (curso == null)
            {
                return BadRequest();
            }

            //Lista de profesores
            var role = await _roleManager.FindByNameAsync("Alumno");

            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();
            bool siuser;
            // Get the list of Users in this Role
            foreach (var user in _userManager.Users.ToList())
            {
                siuser = await _userManager.IsInRoleAsync(user, role.Name);
                if (siuser)
                {
                    users.Add((ApplicationUser)user);
                }
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(x => x.nombre.Contains(SearchString)).ToList();

            }

            ViewBag.profes = users;
            ViewBag.curso = curso;

            return View(curso);
        }

        //Acción para matricula en curso a un estudiante
        // Get

        public ActionResult GeneraMatricula(string idAlum, int? idCurso, string SearchString)
        {
            if (String.IsNullOrEmpty(idAlum) || idCurso == null)
            {
                return BadRequest();
            }

            var alum = db.Users.Find(idAlum);
            var curso = db.Cursoes.Find(idCurso);

            if (alum == null || curso == null)
            {
                return BadRequest();
            }


            ViewBag.alumno = alum;
            ViewBag.curso = curso;

            var grupos = db.CursoProfes.Include(x => x.Curso).Include(x => x.Profe).Where(x => x.Curso.IdCurso == idCurso).ToList();

            if (!String.IsNullOrEmpty(SearchString))
            {
                grupos = grupos.Where(g => g.Profe.nombre.Contains(SearchString)).ToList();
            }

            ViewBag.grupos = grupos;

            return View();
        }

        public ActionResult FinalizaMatricula(int? idGrupo, int? idCurso, string idAlumno)
        {

            if (String.IsNullOrEmpty(idAlumno) || idCurso == null || idGrupo == null)
            {
                return BadRequest();
            }

            var alumno = db.Users.Find(idAlumno);
            var curso = db.Cursoes.Find(idCurso);
            var grupo = db.CursoProfes.Include(x => x.Profe).Include(x => x.Curso).SingleOrDefault(x => x.IdCursoProfe == idGrupo);

            if (alumno == null || curso == null || grupo == null)
            {
                return BadRequest();
            }

            Matricula mat = new Matricula();
            mat.CursoProfe = grupo;
            mat.Alumno = (ApplicationUser)alumno;

            db.Matriculas.Add(mat);
            db.SaveChanges();

            return RedirectToAction("Index", "GruposAdmin");
        }

    }
}
