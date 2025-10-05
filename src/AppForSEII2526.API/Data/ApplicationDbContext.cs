using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<BonoBocadillo> BonoBocadillo { get; set; }
    public DbSet<BonosComprados> BonosComprados { get; set; }
    public DbSet<CompraBono> CompraBono { get; set; }
    public DbSet<TipoBocadillo> TipoBocadillo { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        //CLAVES ALTERNATIVAS
        builder.Entity<BonosComprados>()
            .HasAlternateKey(bc => new { bc.Id, bc.IdCompra });




    }
}
