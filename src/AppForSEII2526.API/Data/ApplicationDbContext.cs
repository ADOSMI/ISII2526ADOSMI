using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

    public class ApplicationDbContext : DbContext
    {

        public DbSet<Bocadillo> Bocadillo { get; set; }
        public DbSet<Resenya> Resenya { get; set; }
        public DbSet<ResenyaBocadillo> ResenyaBocadillo { get; set; }
        public DbSet<TipoPan> TipoPan { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            //CLAVES ALTERNATIVAS
            builder.Entity<ResenyaBocadillo>()
                .HasAlternateKey(rb => new { rb.ResenyaId, rb.BocadilloId });





        }





    }

