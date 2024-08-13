namespace HamburgerUygulamasi2.Entity
{
    public class Malzeme
    {
       public int Id { get; set; }
       public string MalzemeAdi { get; set; }

        public List<Menu>? kullanılanMenu { get; set; }
    }
}
