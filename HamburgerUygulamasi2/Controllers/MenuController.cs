using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerUygulamasi2.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using HamburgerUygulamasi2.Entity;
using System.Security.Claims;
using HamburgerUygulamasi2.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HamburgerUygulamasi2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;

        public MenuController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        // GET: Menu
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menu.ToListAsync());
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuAdi,MenuFiyati")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuAdi,MenuFiyati,ModifiedDate")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    menu.ModifiedDate = DateTime.Now;
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            return View(menu);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu != null)
            {
                _context.Menu.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> SepeteEkle(int id)
        {
            var secilenMenu = await _context.Menu.FindAsync(id);

            var sepetUrun = new SepetUrun()
            {
                Menu = secilenMenu,
                MenuId = secilenMenu.Id,
                Miktar = 1,
            };
            #region enum sipariş boyutu
            var boyutlar = Enum.GetValues(typeof(SepetUrun.Boyut))
                  .Cast<SepetUrun.Boyut>()
                  .Select(b => new SelectListItem
                  {
                      Value = ((int)b).ToString(),
                      Text = b.ToString()
                  }).ToList();

            ViewBag.Boyutlar = boyutlar;
            #endregion
            return View(sepetUrun);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SepeteEkle([FromForm] SepetUrun sepetUrun)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == claim.Value);
            var secilenMenu = _context.Menu.FirstOrDefault(x => x.Id == sepetUrun.MenuId);
            sepetUrun.Menu = secilenMenu;
            _context.Menu.Update(secilenMenu);
            sepetUrun.Id = 0;
            sepetUrun.AraToplamFiyat = sepetUrun.Menu.MenuFiyati * sepetUrun.Miktar;
            bool onayliSiparisVarMi = _context.Siparis.Any(x => (x.SiparisOnayliMi == false) && (x.User.Id==userdb.Id ));
            if (onayliSiparisVarMi)
            {
                var siparis = _context.Siparis.FirstOrDefault(x => (x.SiparisOnayliMi == false) && (x.User.Id == userdb.Id));
                sepetUrun.Siparis = siparis;
                sepetUrun.SiparisId = siparis.Id;
                _context.SepetUrun.Add(sepetUrun);
                siparis.SiparisToplamTutar += sepetUrun.AraToplamFiyat;
                siparis.ModifiedDate = DateTime.Now;
                _context.Siparis.Update(siparis);
            }
            else
            {
                Siparis yeniSiparis = new Siparis()
                {
                    User = userdb,
                    UserId = userdb.Id,
                    SiparisToplamTutar = sepetUrun.AraToplamFiyat,
                };
                sepetUrun.Siparis = yeniSiparis;
                sepetUrun.SiparisId = yeniSiparis.Id;
                _context.SepetUrun.Add(sepetUrun);
                _context.Siparis.Add(yeniSiparis);                
            }
           
            
            
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }




    }
}
