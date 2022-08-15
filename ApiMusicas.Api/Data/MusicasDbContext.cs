using Microsoft.EntityFrameworkCore;
using ApiMusicas.Api.Models;

namespace ApiMusicas.Api.Data;

public class MusicasDbContext : DbContext
{
  private readonly IConfiguration _configuration;

  public MusicasDbContext(IConfiguration configuration)
  {
    _configuration = configuration;

  }
  public DbSet<Artista> Artistas { get; set; }

  public DbSet<Album> Albuns { get; set; }

  public DbSet<Musica> Musicas { get; set; }

  public DbSet<Playlist> Playlists { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseSqlServer(
        _configuration.GetConnectionString("ConexaoBanco")
    );
  }

  // Construção mapeamento
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {

    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Artista>(entidade =>
    {
      entidade.ToTable("Artistas");

      entidade.HasKey(a => a.Id);

      entidade
              .Property(a => a.Nome)
              .HasMaxLength(120)
              .IsRequired();

      entidade
              .Property(a => a.NomeArtistico)
              .HasMaxLength(120);

      entidade
          .Property(a => a.PaisOrigem)
          .HasMaxLength(120);

      entidade
              .HasData(new[]{
                    new Artista {
                        Id = 1,
                        Nome = "James Hetfield",
                        NomeArtistico = "James Hetfield",
                        PaisOrigem = "EUA",

                    },
                    new Artista {
                        Id = 2,
                        Nome = "Kirk Hammett",
                        NomeArtistico = "Kirk Hammett",
                        PaisOrigem = "EUA",
                    }
            });
    });

    modelBuilder.Entity<Album>(entidade =>
   {
     entidade.ToTable("Albuns");

     entidade.HasKey(a => a.Id);

     entidade
             .Property(a => a.Nome)
             .HasMaxLength(120)
             .IsRequired();

     entidade
             .Property(a => a.AnoLancamento)
             .IsRequired();

     entidade
            .HasOne(album => album.Artista)
            .WithMany(artista => artista.Albuns)
            .HasForeignKey(album => album.ArtistaId);
   });

    modelBuilder.Entity<Musica>(entidade =>
    {
      entidade.ToTable("Musicas");

      entidade.HasKey(m => m.Id);

      entidade
              .Property(m => m.Nome)
              .HasMaxLength(120)
              .IsRequired();

      entidade.Property(m => m.Duracao);

      entidade
              .HasOne(m => m.Album)
              .WithMany(a => a.Musicas)
              .HasForeignKey(m => m.AlbumId);

      entidade
              .HasOne(m => m.Artista)
              .WithMany(a => a.Musicas)
              .HasForeignKey(m => m.ArtistaId);
    });

    modelBuilder.Entity<Playlist>(entidade =>
    {
      entidade.ToTable("Playlist");

      entidade.HasKey(p => p.Id);

      entidade
      .Property(p => p.Nome)
      .HasMaxLength(120)
      .IsRequired();

    });

  }
}




