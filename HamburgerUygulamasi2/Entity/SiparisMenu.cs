namespace HamburgerUygulaması.Entity
{
    public class SiparisMenu : BaseEntity
    {
        public Menu Menu { get; set; }
        public int MenuId { get; set; }
        public Siparis Siparis { get; set; }
        public int SiparisId { get; set; }
    }
}
