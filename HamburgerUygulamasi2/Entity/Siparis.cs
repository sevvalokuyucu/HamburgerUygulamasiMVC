using HamburgerUygulaması.Entity;
using HamburgerUygulamasi2.Areas.Identity.Data;
using Microsoft.AspNetCore.DataProtection;

namespace HamburgerUygulamasi2.Entity
{
    public class Siparis:BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public double AraToplam { get; set; }
        public double SiparisToplam { get; set; }
        public string SiparisDurumu {  get; set; }

        public int SepetUrunId { get; set; }
        public SepetUrun sepetUrun { get; set; }

        public int Miktar {  get; set; }

        public string Description { get; set; }
        public double ToplamTutar {  get; set; }



    }
}
