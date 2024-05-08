using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mercy_Dev.Models;

namespace Mercy_Dev.Controllers
{
    public class ClienteHasServiciosController : Controller
    {
        private readonly DevMercyContext _context;

        public ClienteHasServiciosController(DevMercyContext context)
        {
            _context = context;
        }

        // GET: ClienteHasServicios
        public async Task<IActionResult> Index()
        {
            var devMercyContext = _context.ClienteHasServicios.Include(c => c.ClienteIdClienteNavigation).Include(c => c.ServiciosIdServiciosNavigation);
            return View(await devMercyContext.ToListAsync());
        }

        // GET: ClienteHasServicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClienteHasServicios == null)
            {
                return NotFound();
            }

            var clienteHasServicio = await _context.ClienteHasServicios
                .Include(c => c.ClienteIdClienteNavigation)
                .Include(c => c.ServiciosIdServiciosNavigation)
                .FirstOrDefaultAsync(m => m.ClienteIdCliente == id);
            if (clienteHasServicio == null)
            {
                return NotFound();
            }

            return View(clienteHasServicio);
        }

        // GET: ClienteHasServicios/Create
        public IActionResult Create()
        {
            ViewData["ClienteIdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["ServiciosIdServicios"] = new SelectList(_context.Servicios, "IdServicios", "IdServicios");
            return View();
        }

        // POST: ClienteHasServicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteIdCliente,ServiciosIdServicios,FechaHora,Estado")] ClienteHasServicio clienteHasServicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteHasServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteIdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteHasServicio.ClienteIdCliente);
            ViewData["ServiciosIdServicios"] = new SelectList(_context.Servicios, "IdServicios", "IdServicios", clienteHasServicio.ServiciosIdServicios);
            return View(clienteHasServicio);
        }

        // GET: ClienteHasServicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClienteHasServicios == null)
            {
                return NotFound();
            }

            var clienteHasServicio = await _context.ClienteHasServicios.FindAsync(id);
            if (clienteHasServicio == null)
            {
                return NotFound();
            }
            ViewData["ClienteIdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteHasServicio.ClienteIdCliente);
            ViewData["ServiciosIdServicios"] = new SelectList(_context.Servicios, "IdServicios", "IdServicios", clienteHasServicio.ServiciosIdServicios);
            return View(clienteHasServicio);
        }

        // POST: ClienteHasServicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteIdCliente,ServiciosIdServicios,FechaHora,Estado")] ClienteHasServicio clienteHasServicio)
        {
            if (id != clienteHasServicio.ClienteIdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteHasServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteHasServicioExists(clienteHasServicio.ClienteIdCliente))
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
            ViewData["ClienteIdCliente"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", clienteHasServicio.ClienteIdCliente);
            ViewData["ServiciosIdServicios"] = new SelectList(_context.Servicios, "IdServicios", "IdServicios", clienteHasServicio.ServiciosIdServicios);
            return View(clienteHasServicio);
        }

        // GET: ClienteHasServicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClienteHasServicios == null)
            {
                return NotFound();
            }

            var clienteHasServicio = await _context.ClienteHasServicios
                .Include(c => c.ClienteIdClienteNavigation)
                .Include(c => c.ServiciosIdServiciosNavigation)
                .FirstOrDefaultAsync(m => m.ClienteIdCliente == id);
            if (clienteHasServicio == null)
            {
                return NotFound();
            }

            return View(clienteHasServicio);
        }

        // POST: ClienteHasServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClienteHasServicios == null)
            {
                return Problem("Entity set 'DevMercyContext.ClienteHasServicios'  is null.");
            }
            var clienteHasServicio = await _context.ClienteHasServicios.FindAsync(id);
            if (clienteHasServicio != null)
            {
                _context.ClienteHasServicios.Remove(clienteHasServicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteHasServicioExists(int id)
        {
          return (_context.ClienteHasServicios?.Any(e => e.ClienteIdCliente == id)).GetValueOrDefault();
        }
    }
}
