using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventarios.Data;
using MVCInventarios.Models;

namespace MVCInventarios.Controllers
{
    public class PerfilesController : Controller
    {
        private readonly InventariosContext _context;

        public PerfilesController(InventariosContext context)
        {
            _context = context;
        }

        // GET: Perfiles
        public async Task<IActionResult> Index()
        {
              return View(await _context.Perfiles.ToListAsync());
        }

        // GET: Perfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perfiles == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // GET: Perfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Perfil perfil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perfil);
        }

        // GET: Perfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Perfiles == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // POST: Perfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Perfil perfil)
        {
            if (id != perfil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilExists(perfil.Id))
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
            return View(perfil);
        }

        // GET: Perfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perfiles == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfiles == null)
            {
                return Problem("Entity set 'InventariosContext.Perfiles'  is null.");
            }
            var perfil = await _context.Perfiles.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfiles.Remove(perfil);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilExists(int id)
        {
          return _context.Perfiles.Any(e => e.Id == id);
        }
    }
}
