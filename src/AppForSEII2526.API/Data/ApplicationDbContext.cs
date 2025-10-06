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
    public DbSet<Bocadillo> Bocadillo { get; set; }
    public DbSet<Resenya> Resenya { get; set; }
    public DbSet<ResenyaBocadillo> ResenyaBocadillo { get; set; }
    public DbSet<TipoPan> TipoPan { get; set; }
    public DbSet<CompraBocadillo> CompraBocadillo { get; set; }
    public DbSet<Compra> Compra { get; set; }


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

        builder.Entity<ResenyaBocadillo>()
                .HasAlternateKey(rb => new { rb.ResenyaId, rb.BocadilloId });

        builder.Entity<CompraBocadillo>()
            .HasAlternateKey(cb => new { cb.BocadilloId, cb.CompraId });
    }
}





