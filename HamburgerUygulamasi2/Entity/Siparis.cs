

using HamburgerUygulamasi2.Areas.Identity.Data;
using HamburgerUygulaması.Entity;

namespace HamburgerUygulaması2.Entity
{
    public class Siparis : BaseEntity
    {
        public int SiparisAdedi { get; set; }
        public List<Menu> Menuler { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
