using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulamasi2.Entity;
using HamburgerUygulamasi2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HamburgerUygulamasi2.Controllers
{
    public class SepetUrunController : Controller
    {
        private readonly HamburgerUygulamasiContext _context;
        public SepetteUrunSiparisViewModel details { get; set; }

        public SepetUrunController(HamburgerUygulamasiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            details = new SepetteUrunSiparisViewModel()
            {
                siparis = new HamburgerUygulamasi2.Entity.Siparis()
            };
            var userId = HttpContext.Session.GetString("UserId");
           
           // details.sepetUrunleri = await _context.SepetUrun.Where(x=>x.UserId==userId).ToListAsync();
            if (details.sepetUrunleri != null)
            {
                foreach (var item in details.sepetUrunleri)
                {
                    details.siparis.SiparisToplam += (item.Menu.MenuFiyati * item.Miktar);
                }

            }
             return View();
        }
    }
}
