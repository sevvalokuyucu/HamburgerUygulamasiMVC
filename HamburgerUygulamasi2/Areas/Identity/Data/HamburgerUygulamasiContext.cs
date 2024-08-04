﻿using HamburgerUygulaması.Entity;
using HamburgerUygulamasi2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgerUygulamasi2.Areas.Identity.Data;

public class HamburgerUygulamasiContext : IdentityDbContext<User>
{
    public HamburgerUygulamasiContext(DbContextOptions<HamburgerUygulamasiContext> options)
        : base(options)
    {
    }

    public DbSet<Siparis> Siparis { get; set; }
    public DbSet<SiparisMalzeme> SiparisMalzeme { get; set; }
    public DbSet<SiparisMenu> SiparisMenu  { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<EkstraMalzeme> EkstraMalzeme { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
    
