using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<TipoPan> TipoPan { get; set; }
    public DbSet<Bocadillo> Bocadillo { get; set; }
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
        builder.Entity<CompraBocadillo>()
            .HasAlternateKey(cb => new { cb.BocadilloId, cb.CompraId });

    }
}