using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCInventarios.Data;
using MVCInventarios.Models;
using MVCInventarios.ViewModels;
using X.PagedList;

namespace MVCInventarios.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Authorize(Policy = "Administradores")]
    public class PerfilesController : Controller
    {
        private readonly InventariosContext _context;
        private readonly IConfiguration _configuration;
        private readonly INotyfService _servicioNotificacion;

        public PerfilesController(InventariosContext context, IConfiguration configuration,
            INotyfService servicioNotificacion)
        {
            _context = context;
            _configuration = configuration;
            _servicioNotificacion = servicioNotificacion;
        }

        // GET: Perfiles
        public async Task<IActionResult> Index(ListadoViewModel<Perfil> viewModel)
        {
            var registrosPorPagina = _configuration.GetValue("RegistrosPorPagina", 5);

            var consulta = _context.Perfiles
                                            .OrderBy(m => m.Nombre)
                                            .AsQueryable()
                                            .AsNoTracking();

            if (!String.IsNullOrEmpty(viewModel.TerminoBusqueda))
            {
                consulta = consulta.Where(u => u.Nombre.Contains(viewModel.TerminoBusqueda));
            }

            viewModel.TituloCrear = "Crear Perfil";
            viewModel.Total = consulta.Count();

            var numeroPagina = viewModel.Pagina ?? 1;

            viewModel.Registros = await consulta.ToPagedListAsync(numeroPagina, registrosPorPagina);

            return View(viewModel);
        }

        // GET: Perfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Perfiles == null)
            {
                return NotFound();
            }

            var perfil = await _context.Perfiles
                .Include(a => a.Usuarios).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (perfil == null)
            {
                return NotFound();
            }

            return View(perfil);
        }

        //GET
        public async Task<IActionResult> AgregarEditar(int id = 0)
        {
            //Creación
            if (id == 0)
            {
                var perfil = new Perfil();
                return View(perfil);
            }

            //Edición
            var perfilBd = await _context.Perfiles.FindAsync(id);
            if(perfilBd == null)
            {
                return NotFound();
            }

            return View(perfilBd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarEditar([Bind("Id,Nombre")] Perfil perfil)
        {
            if(ModelState.IsValid)
            {
                var existeElementoBd = false;
                //Antes de intentar guardar o actualizar, verificamos que no exista un elemento con el mismo nombre
                if(perfil.Id == 0)
                {
                    existeElementoBd = _context.Perfiles
                                        .Any(u => u.Nombre.ToLower().Trim() == perfil.Nombre.ToLower().Trim());
                }
                else
                {
                    existeElementoBd = _context.Perfiles
                                        .Any(u => u.Nombre.ToLower().Trim() == perfil.Nombre.ToLower().Trim()
                                        && u.Id != perfil.Id);
                }

                if(!existeElementoBd)
                {
                    if(perfil.Id == 0)
                    {
                        //Crear un nuevo elemento
                        _context.Add(perfil);
                        _servicioNotificacion.Success($"Éxito al agregar el perfil {perfil.Nombre}.");
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //Editar el elemento existente
                        try
                        {
                            _context.Update(perfil);
                            _servicioNotificacion.Success($"Éxito al actualizar el perfil {perfil.Nombre}.");
                            await _context.SaveChangesAsync();
                        }
                        catch(DbUpdateConcurrencyException)
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
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _servicioNotificacion.Warning("Lo sentimos, ya existe un perfil con el mismo nombre");
                }
            }
            else
            {
                var accion = perfil.Id == default ? "agregar" : "actualizar";
                _servicioNotificacion.Error("Es necesario corregir os problemas para poder "+ accion +" el perfil.");
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
                return Problem("Entity set 'InventariosContext.Perfil'  is null.");
            }
            var perfil = await _context.Perfiles.FindAsync(id);
            if (perfil != null)
            {
                _context.Perfiles.Remove(perfil);
            }

            await _context.SaveChangesAsync();
            _servicioNotificacion.Success($"Éxito al eliminar el perfil {perfil.Nombre}");
            return RedirectToAction(nameof(Index));
        }

        private bool PerfilExists(int id)
        {
          return _context.Perfiles.Any(e => e.Id == id);
        }
    }
}
