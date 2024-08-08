using HamburgerUygulaması.Entity;
using HamburgerUygulamasi2.Entity;

namespace HamburgerUygulamasi2.Models
{
    public class SepetteUrunSiparisViewModel:BaseEntity
    {
        public List<SepetUrun> sepetUrunleri;
        
        public Siparis siparis { get; set; }
    }
}
