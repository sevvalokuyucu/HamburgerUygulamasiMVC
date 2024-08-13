using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulamasi2.Entity;
using System.Security.Claims;

namespace HamburgerUygulamasi2.Controllers
{
    public class SepetUrunsController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;

        public SepetUrunsController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        // GET: SepetUruns
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == claim.Value);
            var sepetUrunleri = _context.SepetUrun.Include(s => s.Menu).Include(s => s.Siparis).Where(s=>s.Siparis.UserId==userdb.Id);
            return View(await sepetUrunleri.ToListAsync());
        }

        // GET: SepetUruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetUrun = await _context.SepetUrun
                .Include(s => s.Menu)
                .Include(s => s.Siparis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepetUrun == null)
            {
                return NotFound();
            }

            return View(sepetUrun);
        }

        // GET: SepetUruns/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id");
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id");
            return View();
        }

        // POST: SepetUruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,Miktar,boyut,SiparisId,AraToplamFiyat,EkstraMalzemeId")] SepetUrun sepetUrun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepetUrun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", sepetUrun.MenuId);
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", sepetUrun.SiparisId);
            return View(sepetUrun);
        }

        // GET: SepetUruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetUrun = await _context.SepetUrun.FindAsync(id);
            if (sepetUrun == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", sepetUrun.MenuId);
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", sepetUrun.SiparisId);
            return View(sepetUrun);
        }

        // POST: SepetUruns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,Miktar,boyut,SiparisId,AraToplamFiyat,EkstraMalzemeId")] SepetUrun sepetUrun)
        {
            if (id != sepetUrun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepetUrun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetUrunExists(sepetUrun.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", sepetUrun.MenuId);
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "Id", "Id", sepetUrun.SiparisId);
            return View(sepetUrun);
        }

        // GET: SepetUruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepetUrun = await _context.SepetUrun
                .Include(s => s.Menu)
                .Include(s => s.Siparis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepetUrun == null)
            {
                return NotFound();
            }

            return View(sepetUrun);
        }

        // POST: SepetUruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepetUrun = await _context.SepetUrun.FindAsync(id);
            if (sepetUrun != null)
            {
                _context.SepetUrun.Remove(sepetUrun);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetUrunExists(int id)
        {
            return _context.SepetUrun.Any(e => e.Id == id);
        }
    }
}
