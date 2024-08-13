using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulamasi2.Entity;

namespace HamburgerUygulamasi2.Controllers
{
    public class EkstraMalzemesController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;

        public EkstraMalzemesController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        // GET: EkstraMalzemes
        public async Task<IActionResult> Index()
        {

            return View(await _context.EkstraMalzeme.ToListAsync());
        }

        // GET: EkstraMalzemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await _context.EkstraMalzeme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }

            return View(ekstraMalzeme);
        }

        // GET: EkstraMalzemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EkstraMalzemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MalzemeAdi,MalzemeFiyati")] EkstraMalzeme ekstraMalzeme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekstraMalzeme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ekstraMalzeme);
        }

        // GET: EkstraMalzemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await _context.EkstraMalzeme.FindAsync(id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }
            return View(ekstraMalzeme);
        }

        // POST: EkstraMalzemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MalzemeAdi,MalzemeFiyati")] EkstraMalzeme ekstraMalzeme)
        {
            if (id != ekstraMalzeme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekstraMalzeme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkstraMalzemeExists(ekstraMalzeme.Id))
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
            return View(ekstraMalzeme);
        }

        // GET: EkstraMalzemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await _context.EkstraMalzeme
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }

            return View(ekstraMalzeme);
        }

        // POST: EkstraMalzemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekstraMalzeme = await _context.EkstraMalzeme.FindAsync(id);
            if (ekstraMalzeme != null)
            {
                _context.EkstraMalzeme.Remove(ekstraMalzeme);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkstraMalzemeExists(int id)
        {
            return _context.EkstraMalzeme.Any(e => e.Id == id);
        }
    }
}
