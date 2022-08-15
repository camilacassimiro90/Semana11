using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;
namespace ApiMusicas.Api.ViewModels;
public class MusicaCompletaViewModel
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public TimeSpan Duracao { get; set; }
  public AlbumSimplesViewModel Album { get; set; }
  public ArtistaViewModel Artista { get; set; }

  public MusicaCompletaViewModel(Musica musica)
  {
    Id = musica.Id;
    Nome = musica.Nome;
    Duracao = musica.Duracao;
    Album = musica.Album == null ? null : new AlbumSimplesViewModel(musica.Album);
    Artista = new ArtistaViewModel(musica.Artista);
  }
}