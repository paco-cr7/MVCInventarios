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
    public class ProductosController : Controller
    {
        private readonly InventariosContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _servicioNotificacion;

        public ProductosController(InventariosContext context, IConfiguration configuration,
            INotyfService servicioNotificacion)
        {
            _context = context;
            _configuration = configuration;
            _servicioNotificacion = servicioNotificacion;
        }

        // GET: Productos
        public async Task<IActionResult> Index(ListadoViewModel<Producto> viewModel)
        {
            var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 5);

            var consulta = _context.Productos
                                            .Include(a => a.Marca)
                                            .OrderBy(m => m.Nombre)
                                            .AsQueryable().AsNoTracking();

            if (!String.IsNullOrEmpty(viewModel.TerminoBusqueda))
            {
                consulta = consulta.Where(u => u.Nombre.Contains(viewModel.TerminoBusqueda)
                                            || u.Marca.Nombre.Contains(viewModel.TerminoBusqueda));
            }

            viewModel.TituloCrear = "Crear Productos";
            viewModel.Total = consulta.Count();

            var numeroPagina = viewModel.Pagina ?? 1;

            viewModel.Registros = await consulta.ToPagedListAsync(numeroPagina, registrosPorPagina);

            return View(viewModel);
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            AgregarEditarProductoViewModel viewModel = new AgregarEditarProductoViewModel();
            viewModel.ListadoMarcas = new SelectList(_context.Marcas.AsNoTracking(), "Id", "Nombre");
            return View("Producto", viewModel);
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,MarcaId,Costo,Estatus")] Producto producto)
        {
            AgregarEditarProductoViewModel viewModel = new AgregarEditarProductoViewModel();
            viewModel.ListadoMarcas = new SelectList(_context.Marcas.AsNoTracking(), "Id", "Nombre", producto.MarcaId);
            viewModel.Producto = producto;

            if (ModelState.IsValid)
            {
                var exiteElementoBd = _context.Productos.Any(u => u.Nombre.ToLower().Trim() == producto.Nombre.ToLower().Trim());

                if (exiteElementoBd)
                {
                    ModelState.AddModelError("", "Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe un elemento con el nombre indicado.");
                    return View("Producto", viewModel);
                }

                try
                {
                    _context.Add(producto);
                    await _context.SaveChangesAsync();
                    _servicioNotificacion.Success($"Éxito al crear el producto {producto.Nombre}");
                }
                catch (DbUpdateException)
                {
                    //ModelState.AddModelError("","Lo sentimos, ha ocurrido un error. Intente nuevamente");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe un elemento con el nombre indicado.");
                    return View("Producto", viewModel);
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Producto", viewModel);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            AgregarEditarProductoViewModel viewModel = new AgregarEditarProductoViewModel();
            viewModel.ListadoMarcas = new SelectList(_context.Marcas.AsNoTracking() ,"Id" , "Nombre", producto.MarcaId);
            viewModel.Producto = producto;
            return View("Producto", viewModel);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,MarcaId,Costo,Estatus")] Producto producto)
        {
            AgregarEditarProductoViewModel viewModel = new AgregarEditarProductoViewModel();
            viewModel.ListadoMarcas = new SelectList(_context.Marcas.AsNoTracking(), "Id", "Nombre", producto.MarcaId);
            viewModel.Producto = producto;

            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var exiteElementoBd = _context.Productos.Any(u => u.Nombre.ToLower().Trim() == producto.Nombre.ToLower().Trim() && u.Id != producto.Id);

                if (exiteElementoBd)
                {
                    ModelState.AddModelError("", "Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    _servicioNotificacion.Warning("Lo sentimos. Ya existe une lemento con el nombre indicado.");
                    return View("Producto", viewModel);
                }

                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    _servicioNotificacion.Success($"Éxito al actualizar el producto {producto.Nombre}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View("Producto", viewModel); ;
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'InventariosContext.Producto'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            _servicioNotificacion.Success($"Éxito al eliminar el producto {producto.Nombre}");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return _context.Productos.Any(e => e.Id == id);
        }
    }
}
