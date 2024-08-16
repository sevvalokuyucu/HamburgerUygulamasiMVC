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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging;

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
            var malzemeler = _context.Malzeme.ToList();
            if(malzemeler!=null)
                TempData["Malzemeler"] = malzemeler;
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel menuViewModel)
        {


            try
            {
                Menu menu = new Menu();
                if (menuViewModel.ImageUrl != null)
                {
                    var fileName = menuViewModel.ImageUrl.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", fileName);

                    var stream = new FileStream(path, FileMode.Create);

                    menuViewModel.ImageUrl.CopyTo(stream);

                    stream.Close();

                    menu.ImageName = fileName;

                }
                menu.MenuAdi = menuViewModel.MenuAdi;
                menu.MenuFiyati = menuViewModel.MenuFiyati;
                

                _context.Menu.Add(menu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) { return View(); }
            
   
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            Menu updatedMenu = _context.Menu.Find(id);

            TempData["Id"] = updatedMenu.Id;

            MenuViewModel menuViewModel = new MenuViewModel();

            menuViewModel.MenuFiyati = updatedMenu.MenuFiyati;

            menuViewModel.MenuAdi = updatedMenu.MenuAdi;

            ViewBag.ImageName = updatedMenu.ImageName;


            return View(menuViewModel);

        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuViewModel menuViewModel)
        {

            Menu updatedMenu = _context.Menu.Find(TempData["Id"]);

            try

            {

                if (menuViewModel.ImageUrl != null && menuViewModel.ImageUrl.FileName != updatedMenu.ImageName)

                {

                    DeleteImage(updatedMenu);

                    var fileName = menuViewModel.ImageUrl.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", fileName);

                    var stream = new FileStream(path, FileMode.Create);

                    menuViewModel.ImageUrl.CopyTo(stream);

                    stream.Close();

                    updatedMenu.ImageName = fileName;

                }

                updatedMenu.MenuFiyati = menuViewModel.MenuFiyati;
                updatedMenu.MenuAdi = menuViewModel.MenuAdi;
                updatedMenu.ModifiedDate = DateTime.Now;



                _context.Menu.Update(updatedMenu);

                _context.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                TempData["Status"] = "Error occured! " + ex.Message;

                return View();

            }
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
                      Text = b.ToString().Replace("_", " ").Replace("w", "+")

                  }).ToList();

            ViewBag.Boyutlar = boyutlar;
            var ekstraMalzemes = _context.EkstraMalzeme.ToList();
            if (ekstraMalzemes != null)
                ViewData["ekstraMalzemeler"]=ekstraMalzemes;
            #endregion
            return View(sepetUrun);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SepeteEkle([FromForm] SepetUrun sepetUrun, int[] secilenMalzemeler)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == claim.Value);
            var secilenMenu = _context.Menu.FirstOrDefault(x => x.Id == sepetUrun.MenuId);
            sepetUrun.Menu = secilenMenu;
            _context.Menu.Update(secilenMenu);
            sepetUrun.Id = 0;
            sepetUrun.AraToplamFiyat = sepetUrun.Menu.MenuFiyati * sepetUrun.Miktar;
            List<EkstraMalzeme> extramalzemes = new List<EkstraMalzeme>();
            foreach(var item in secilenMalzemeler)
            {
                var ekstraMalzeme = _context.EkstraMalzeme.Find(item);
                extramalzemes.Add(ekstraMalzeme);
            }
            sepetUrun.ekstraMalzemeler = extramalzemes;
            bool onayliSiparisVarMi = _context.Siparis.Any(x => (x.SiparisOnayliMi == false) && (x.User.Id==userdb.Id ));
            if (onayliSiparisVarMi)
            {
                var siparis = _context.Siparis.FirstOrDefault(x => (x.SiparisOnayliMi == false) && (x.User.Id == userdb.Id));
                sepetUrun.Siparis = siparis;
                sepetUrun.SiparisId = siparis.Id;
                _context.SepetUrun.Add(sepetUrun);
                await _context.SaveChangesAsync();
                var urun = _context.SepetUrun.OrderByDescending(x => x.Id).First();
                foreach (var item in extramalzemes)
                {
                    _context.SepetUrunMalzemeler.Add(new SepetUrunMalzeme
                    {
                        SepetUrunId = urun.Id,
                        EkstraMalzemeId = item.Id
                    });
                }
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


        public void DeleteImage(Menu menu)

        {

            var otherMenuExist = _context.Menu.Any(p => p.ImageName == menu.ImageName && p.Id != menu.Id);

            if (menu.ImageName != null && !otherMenuExist)

            {

                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", menu.ImageName);

                System.IO.File.Delete(file);

            }

        }


    }
}
