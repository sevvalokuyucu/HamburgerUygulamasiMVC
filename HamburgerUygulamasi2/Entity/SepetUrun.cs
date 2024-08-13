using HamburgerUygulamasi2.Areas.Identity.Data;

namespace HamburgerUygulamasi2.Entity
{
    public class SepetUrun
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int Miktar { get; set; }

        public Boyut boyut { get; set; }
        public enum Boyut { Küçük, Orta, Büyük }

        public Siparis Siparis { get; set; }
        public int SiparisId { get; set; }

        public double AraToplamFiyat { get; set; }

        public int? EkstraMalzemeId { get; set; }
        public List<EkstraMalzeme>? ekstraMalzemeler { get; set; }


    }
}
