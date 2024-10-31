namespace HamburgerUygulamasi2.Entity
{
    public class Menu : BaseEntity
    {
        public string MenuAdi { get; set; }
        public double MenuFiyati { get; set; }
        public string? ImageName {  get; set; }
        public List<Malzeme>? MenuMalzemeler {  get; set; }
        
    }
}
