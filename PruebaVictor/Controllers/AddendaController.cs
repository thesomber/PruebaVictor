using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaVictor.Models;

namespace PruebaVictor.Controllers
{
    public class AddendaController : Controller
    {
        private readonly CerAddendasContext _context;

        public AddendaController(CerAddendasContext context)
        {
            _context = context;
        }

        // GET: Addenda
        public async Task<IActionResult> Index()
        {
              return _context.Addendas != null ? 
                          View(await _context.Addendas.ToListAsync()) :
                          Problem("Entity set 'CerAddendasContext.Addendas'  is null.");
        }

        // GET: Addenda/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas
                .FirstOrDefaultAsync(m => m.IdAddenda == id);
            if (addenda == null)
            {
                return NotFound();
            }

            return View(addenda);
        }

        // GET: Addenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAddenda,NombreAddenda,Xml,FechaModificacion,Usuario,Estado")] Addenda addenda)
        {
            if (ModelState.IsValid)
            {
                addenda.IdAddenda = Guid.NewGuid();
                _context.Add(addenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addenda);
        }

        // GET: Addenda/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas.FindAsync(id);
            if (addenda == null)
            {
                return NotFound();
            }
            return View(addenda);
        }

        // POST: Addenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdAddenda,NombreAddenda,Xml,FechaModificacion,Usuario,Estado")] Addenda addenda)
        {
            if (id != addenda.IdAddenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddendaExists(addenda.IdAddenda))
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
            return View(addenda);
        }

        // GET: Addenda/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Addendas == null)
            {
                return NotFound();
            }

            var addenda = await _context.Addendas
                .FirstOrDefaultAsync(m => m.IdAddenda == id);
            if (addenda == null)
            {
                return NotFound();
            }

            return View(addenda);
        }

        // POST: Addenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Addendas == null)
            {
                return Problem("Entity set 'CerAddendasContext.Addendas'  is null.");
            }
            var addenda = await _context.Addendas.FindAsync(id);
            if (addenda != null)
            {
                _context.Addendas.Remove(addenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddendaExists(Guid id)
        {
          return (_context.Addendas?.Any(e => e.IdAddenda == id)).GetValueOrDefault();
        }
    }
}
