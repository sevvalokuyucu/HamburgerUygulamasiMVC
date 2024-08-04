using System.Reflection.PortableExecutable;

namespace HamburgerUygulaması.Entity
{
    public class EkstraMalzeme : BaseEntity
    {
        public string MalzemeAdi { get; set; }
        public double MalzemeFiyati { get; set; }
        public List<SiparisMalzeme> SiparisMalzeme { get; set; }
    }
}
