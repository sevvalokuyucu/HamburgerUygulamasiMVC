using HamburgerUygulaması.Entity;
using HamburgerUygulamasi2.Areas.Identity.Data;

namespace HamburgerUygulamasi2.Entity
{
    public class SepetUrun
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public int Miktar { get; set; }

        public int? EkstraMalzemeId { get; set; }
        public List<EkstraMalzeme>? ekstraMalzemeler { get; set; }
    }
}
