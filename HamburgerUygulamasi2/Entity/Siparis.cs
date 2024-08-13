using HamburgerUygulamasi2.Entity;
using HamburgerUygulamasi2.Areas.Identity.Data;
using Microsoft.AspNetCore.DataProtection;

namespace HamburgerUygulamasi2.Entity
{
    public class Siparis:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public double AraToplam { get; set; }
        public double SiparisToplamTutar { get; set; }
        public bool SiparisOnayliMi {  get; set; }

        public List<SepetUrun> SepetUrunleri { get; set; }

        public int Miktar {  get; set; }

        public string? Aciklama { get; set; }



    }
}
