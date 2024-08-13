using HamburgerUygulamasi2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HamburgerUygulamasi2.Entity
{
    public class SepetUrun
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int Miktar { get; set; }

        public Boyut boyut { get; set; }
        public enum Boyut
        {
            [Display(Name = "Küçük")]
            Küçük,
            [Display(Name = "Orta (+25 TL)")]
            Orta_w25TL,
            [Display(Name = "Büyük (+45 TL")]
            Büyük_w45TL
        }

        public Siparis? Siparis { get; set; }
        public int? SiparisId { get; set; }

        private double araToplamF;
        public double AraToplamFiyat
        {
            get { return araToplamF; }
            set {
                if (boyut == Boyut.Orta_w25TL)
                    araToplamF = value + 25;
                else if (boyut == Boyut.Büyük_w45TL)
                    araToplamF = value + 45;
                else
                    araToplamF = value;
            }
            
        }
        public List<EkstraMalzeme>? ekstraMalzemeler { get; set; }


    }
}
