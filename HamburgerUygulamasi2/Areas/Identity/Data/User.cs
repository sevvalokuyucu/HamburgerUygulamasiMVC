using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using HamburgerUygulaması.Entity;
using Microsoft.AspNetCore.Identity;

namespace HamburgerUygulamasi2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public List<Siparis> Siparis { get; set; }

}

