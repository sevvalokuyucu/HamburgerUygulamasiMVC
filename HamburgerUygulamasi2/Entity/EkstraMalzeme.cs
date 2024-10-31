using System.Reflection.PortableExecutable;

namespace HamburgerUygulamasi2.Entity
{
    public class EkstraMalzeme 
    {
        public int Id { get; set; }
        public string MalzemeAdi { get; set; }
        public double MalzemeFiyati { get; set; }

        public List<SepetUrun>? sepetUruns { get; set; }

    }
}
