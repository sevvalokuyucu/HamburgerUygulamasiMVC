﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulaması.Entity;
using Microsoft.AspNetCore.Authorization;
using HamburgerUygulamasi2.Entity;
using System.Security.Claims;
using HamburgerUygulamasi2.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == claim.Value);
            
            var sepetUrun = new SepetUrun()
            {
                Menu = secilenMenu,
                MenuId = secilenMenu.Id,
                Miktar = 1,
                UserId = userdb.Id,
                User = userdb
            };
            return View(sepetUrun);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SepeteEkle([FromForm] SepetUrun sepetUrun)
        {
            sepetUrun.Menu = _context.Menu.FirstOrDefault(x => x.Id == sepetUrun.MenuId);
            sepetUrun.User = _context.Users.FirstOrDefault(x => x.Id == sepetUrun.UserId);
            sepetUrun.Id = 0;
            var sepetteUrunDb = await _context.SepetUrun.Where(x => x.UserId == sepetUrun.UserId && x.MenuId == sepetUrun.MenuId).FirstOrDefaultAsync();
                if (sepetteUrunDb == null)
                {
                    await _context.SepetUrun.AddAsync(sepetUrun);
                }
                else
                {
                    sepetteUrunDb.Miktar = sepetteUrunDb.Miktar + sepetUrun.Miktar;
                }

            var details = new SepetteUrunSiparisViewModel()
            {
                siparis = new HamburgerUygulamasi2.Entity.Siparis()
            };


            await _context.SaveChangesAsync();
                return RedirectToAction("Index","SepetUrun");
        }
        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }




    }
}
