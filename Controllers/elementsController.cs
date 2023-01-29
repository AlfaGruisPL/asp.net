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
    public class elementsController : Controller
    {
        private readonly zpnetContext _context;

        public elementsController(zpnetContext context)
        {
            _context = context;
        }

        // GET: elements
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Elementy.Include(tmp=>tmp.kategorie).Include(tmp => tmp.miejsce).ToListAsync());
        }

        // GET: elements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Elementy == null)
            {
                return NotFound();
            }

            var element = await _context.Elementy
                .FirstOrDefaultAsync(m => m.id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // GET: elements/Create
        public IActionResult Create()
        {
            ViewData["miejsca"] = new SelectList(_context.Miejsca, "id", "nazwa");
            ViewData["kategorie"] = _context.Kategorie;
            return View();
        }

        // POST: elements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nazwa,opis,Data_stworzenia,miejsceId")] element element)
        {
            if (ModelState.IsValid)
            {
                _context.Add(element);
                await _context.SaveChangesAsync();


                var WybraneKsiazki = HttpContext.Request.Form["kategorie"];

                foreach (var id in WybraneKsiazki)
                {
                    var Content = await _context.Kategorie.Include(k => k.elementy).SingleAsync(kategoria => kategoria.id == int.Parse(id));
                    Content.elementy.Add(element);
                    _context.Update(Content);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }

            return View(element);
        }

        // GET: elements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Elementy == null)
            {
                return NotFound();
            }

            var element = await _context.Elementy.Include(tmp=>tmp.kategorie).SingleAsync(tmp=>tmp.id == id);
            if (element == null)
            {
                return NotFound();
            }

            var lista = _context.Kategorie;
            foreach (var kategoria in lista)
            {
                foreach (var tmo in element.kategorie)
                {
                    if (kategoria.id == tmo.id)
                    {
                        kategoria.check = true;
                    }

                }
            }



            ViewData["miejsca"] = new SelectList(_context.Miejsca, "id", "nazwa");
            ViewData["kategorie"] = lista;
            return View(element);
        }

        // POST: elements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nazwa,opis,Data_stworzenia,miejsceId")] element element)
        {
            if (id != element.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(element);
                    await _context.SaveChangesAsync();

                    var elementTmp = await _context.Elementy.Include(tmp => tmp.kategorie).SingleAsync(tmp => tmp.id == element.id);
                    if (elementTmp.kategorie != null)
                    {
                        elementTmp.kategorie.Clear();
                    }
                    else
                    {
                        elementTmp.kategorie = new List<kategoria>();
                    };
                    _context.Update(elementTmp);
                    await _context.SaveChangesAsync();


                    var zaznaczone = HttpContext.Request.Form["kategorie"];

                    foreach (var zaznaczoneid in zaznaczone)
                    {
                        var Content = await _context.Kategorie.Include(tmp => tmp.elementy).SingleAsync(tmp => tmp.id == int.Parse(zaznaczoneid));
                        Content.elementy.Add(element);
                        _context.Update(Content);
                        await _context.SaveChangesAsync();
                    }



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!elementExists(element.id))
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
            return View(element);
        }

        // GET: elements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Elementy == null)
            {
                return NotFound();
            }

            var element = await _context.Elementy
                .FirstOrDefaultAsync(m => m.id == id);
            if (element == null)
            {
                return NotFound();
            }

            return View(element);
        }

        // POST: elements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Elementy == null)
            {
                return Problem("Entity set 'zpnetContext.Elementy'  is null.");
            }
            var element = await _context.Elementy.FindAsync(id);
            if (element != null)
            {
                _context.Elementy.Remove(element);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool elementExists(int id)
        {
          return _context.Elementy.Any(e => e.id == id);
        }
    }
}
