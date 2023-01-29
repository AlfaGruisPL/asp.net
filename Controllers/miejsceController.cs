using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zpnet.Models;

namespace zpnet.Controllers
{
    public class miejsceController : Controller
    {
        private readonly zpnetContext _context;

        public miejsceController(zpnetContext context)
        {
            _context = context;
        }

        // GET: miejsces
        public async Task<IActionResult> Index()
        {
              return View(await _context.Miejsca.Include(tmp => tmp.elementy).ToListAsync());
        }

        // GET: miejsces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Miejsca == null)
            {
                return NotFound();
            }

            var miejsce = await _context.Miejsca
                .FirstOrDefaultAsync(m => m.id == id);
            if (miejsce == null)
            {
                return NotFound();
            }

            return View(miejsce);
        }

        // GET: miejsces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: miejsces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nazwa")] miejsce miejsce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miejsce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miejsce);
        }

        // GET: miejsces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Miejsca == null)
            {
                return NotFound();
            }

            var miejsce = await _context.Miejsca.FindAsync(id);
            if (miejsce == null)
            {
                return NotFound();
            }
            return View(miejsce);
        }

        // POST: miejsces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nazwa")] miejsce miejsce)
        {
            if (id != miejsce.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miejsce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!miejsceExists(miejsce.id))
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
            return View(miejsce);
        }

        // GET: miejsces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Miejsca == null)
            {
                return NotFound();
            }

            var miejsce = await _context.Miejsca
                .FirstOrDefaultAsync(m => m.id == id);
            if (miejsce == null)
            {
                return NotFound();
            }

            return View(miejsce);
        }

        // POST: miejsces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Miejsca == null)
            {
                return Problem("Entity set 'zpnetContext.Miejsca'  is null.");
            }
            var miejsce = await _context.Miejsca.FindAsync(id);
            if (miejsce != null)
            {
                _context.Miejsca.Remove(miejsce);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool miejsceExists(int id)
        {
          return _context.Miejsca.Any(e => e.id == id);
        }
    }
}
