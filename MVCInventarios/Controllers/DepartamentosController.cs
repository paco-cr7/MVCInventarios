using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventarios.Data;
using MVCInventarios.Models;
using MVCInventarios.ViewModels;
using X.PagedList;

namespace MVCInventarios.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly InventariosContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _servicioNotificacion;

        public DepartamentosController(InventariosContext context, IConfiguration configuration,
            INotyfService servicioNotificacion)
        {
            _context = context;
            _configuration = configuration;
            _servicioNotificacion = servicioNotificacion;
        }

        // GET: Departamentos
        public async Task<IActionResult> Index(ListadoViewModel<Departamento> viewModel)
        {
            var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 5);

            var consulta = _context.Departamentos.OrderBy(m => m.Nombre).AsQueryable().AsNoTracking();

            if (!String.IsNullOrEmpty(viewModel.TerminoBusqueda))
            {
                consulta = consulta.Where(u => u.Nombre.Contains(viewModel.TerminoBusqueda));
            }

            viewModel.TituloCrear = "Crear Departamentos";
            viewModel.Total = consulta.Count();

            var numeroPagina = viewModel.Pagina ?? 1;

            viewModel.Registros = await consulta.ToPagedListAsync(numeroPagina, registrosPorPagina);

            return View(viewModel);
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,FechaCreacion")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                var exiteElementoBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == departamento.Nombre.ToLower().Trim());

                if (exiteElementoBd)
                {
                    //ModelState.AddModelError("", "Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe un elemento con el nombre indicado.");
                    return View(departamento);
                }

                try
                {
                    departamento.FechaCreacion = DateTime.Now;
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    _servicioNotificacion.Success($"Éxito al crear el departamento {departamento.Nombre}");
                }
                catch (DbUpdateException)
                {
                    //ModelState.AddModelError("","Lo sentimos, ha ocurrido un error. Intente nuevamente");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe un elemento con el nombre indicado.");
                    return View(departamento);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var exiteElementoBd = _context.Marcas.Any(u => u.Nombre.ToLower().Trim() == departamento.Nombre.ToLower().Trim() && u.Id != departamento.Id);

                if (exiteElementoBd)
                {
                    //ModelState.AddModelError("", "Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    return View(departamento);
                }

                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                    _servicioNotificacion.Success($"Éxito al actualizar el departamento {departamento.Nombre}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.Id))
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
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departamentos == null)
            {
                return Problem("Entity set 'InventariosContext.Departamento'  is null.");
            }
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
            }

            await _context.SaveChangesAsync();
            _servicioNotificacion.Success($"Éxito al eliminar el departamento {departamento.Nombre}");
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
          return _context.Departamentos.Any(e => e.Id == id);
        }
    }
}
