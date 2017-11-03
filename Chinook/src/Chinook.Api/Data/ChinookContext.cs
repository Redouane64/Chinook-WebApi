using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chinook.Api.Data
{
    public partial class ChinookContext : DbContext
    {

		public ChinookContext(DbContextOptions<ChinookContext> options) 
			: base(options)
		{
		}

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(e => e.ArtistId)
                    .HasName("IFK_AlbumArtistId");

                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Album)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumArtistId");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId).ValueGeneratedNever();
            });
        }
    }
}
