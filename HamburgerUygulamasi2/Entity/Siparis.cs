

using HamburgerUygulamasi2.Areas.Identity.Data;

namespace HamburgerUygulaması.Entity
{
    public class Siparis : BaseEntity
    {
        public List<Menu> SiparisMenuleri { get; set; }
        public List<EkstraMalzeme>? EkstraMalzemeler { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
