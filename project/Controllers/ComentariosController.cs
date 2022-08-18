using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

    

        // POST: Comentarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                String idUsu = collection["usuario"];
                int idLec = Int32.Parse(collection["leccion"]);
                String Contenido = collection["contenido"];

                ApplicationUser usuario = (ApplicationUser)_context.Users.Find(idUsu);
                Lecciones lec = _context.Lecciones.Find(idLec);

                Comentarios coment = new Comentarios();
                coment.Comentario = Contenido;
                coment.Usuario = usuario;
                coment.Leccion = lec;

                _context.Comentarios.Add(coment);
                _context.SaveChanges();


                return RedirectToAction("Details", "Lecciones", new { id = idLec });
            }
            catch
            {
                return View();
            }
        }


        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comentarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Comentarios'  is null.");
            }
            var comentarios = await _context.Comentarios.FindAsync(id);
            if (comentarios != null)
            {
                _context.Comentarios.Remove(comentarios);
            }
            
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
