namespace HamburgerUygulamasi2.Entity
{
    public class SepetUrunMalzeme:BaseEntity
    {
        public SepetUrun SepetUrun { get; set; }
        public int SepetUrunId { get; set; }
        public EkstraMalzeme EkstraMalzeme { get; set; }
        public int EkstraMalzemeId { get; set; }
    }
}
