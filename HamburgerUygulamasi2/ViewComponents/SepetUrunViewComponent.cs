using HamburgerUygulamasi2.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerUygulamasi2.ViewComponents
{
    public class SepetUrunViewComponent : ViewComponent
    {
        private readonly HamburgerUygulamasiContext _context;

        public SepetUrunViewComponent(HamburgerUygulamasiContext context)
        {
            _context = context;
        }
    }
}