namespace HamburgerUygulaması.Entity
{
    public class SiparisMalzeme : BaseEntity
    {
        public int SiparisId { get; set; }
        public Siparis Siparis { get; set; }
        public int EkstraMalzemeId { get; set; }
        public EkstraMalzeme EkstraMalzeme { get; set; }

    }
}
