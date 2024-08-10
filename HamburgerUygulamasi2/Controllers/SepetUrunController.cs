using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulamasi2.Entity;
using HamburgerUygulamasi2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HamburgerUygulamasi2.Controllers
{
    public class SepetUrunController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;
 
        public SepetUrunController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SepetteUrunSiparisViewModel details)
        {
            details = new SepetteUrunSiparisViewModel()
            {
                siparis = new HamburgerUygulamasi2.Entity.Siparis()
            };
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userdb = await _context.Users.FirstOrDefaultAsync(x => x.Id == claim.Value);

            details.sepetUrunleri = await _context.SepetUrun.Where(x=>x.UserId==userdb.Id).ToListAsync();
            if (details.sepetUrunleri != null)
            {
                foreach (var item in details.sepetUrunleri)
                {
                    details.siparis.SiparisToplam += (item.Menu.MenuFiyati * item.Miktar);
                }

            }
            await _context.SepetteUrunSiparisViewModel.AddAsync(details);
            await _context.SaveChangesAsync();  
             return View(details);
        }
    }
}
