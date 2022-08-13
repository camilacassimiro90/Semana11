using Microsoft.EntityFrameworkCore;
using MusicApplication.Api.Models;

namespace MusicApplication.Api.Data;

public class MusicasDbContext : DbContext
{
    public DbSet <Artista> Artistas { get; set; }

    public DbSet <Album> Albuns { get; set; }

    public DbSet <Musica> Musicas { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            _configuration.GetConnectionString("ConexaoBanco")
        );
    }
}
