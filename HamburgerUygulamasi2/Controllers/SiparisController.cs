using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulaması.Entity;
using Microsoft.AspNetCore.SignalR;

namespace HamburgerUygulamasi2.Controllers
{
    public class SiparisController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;

        public SiparisController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        // GET: Siparis
        public async Task<IActionResult> Index()
        {
            var hamburgerUygulamasiContext = _context.Siparis.Include(s => s.User);
            return View(await hamburgerUygulamasiContext.ToListAsync());
        }

        // GET: Siparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // GET: Siparis/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Siparis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, Id")] List<Menu> SecilenMenuler)
        {
            if (ModelState.IsValid)
            {
                Siparis siparis = new Siparis();
                siparis.SiparisMenuleri = SecilenMenuler;
                _context.Add(siparis);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", siparis.UserId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
               
            }
            
            return RedirectToAction("Index","Menu");
        }

        // GET: Siparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", siparis.UserId);
            return View(siparis);
        }

        // POST: Siparis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Id,CreatedDate,ModifiedDate")] Siparis siparis)
        {
            if (id != siparis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisExists(siparis.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", siparis.UserId);
            return View(siparis);
        }

        // GET: Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparis
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparis = await _context.Siparis.FindAsync(id);
            if (siparis != null)
            {
                _context.Siparis.Remove(siparis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            return _context.Siparis.Any(e => e.Id == id);
        }
    }
}
