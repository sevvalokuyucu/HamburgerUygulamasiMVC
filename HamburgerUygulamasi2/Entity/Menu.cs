﻿namespace HamburgerUygulaması.Entity
{
    public class Menu : BaseEntity
    {
        public string MenuAdi { get; set; }
        public double MenuFiyati { get; set; }
        public List<SiparisMenu?> SiparisMenu { get; set; }
    }
}