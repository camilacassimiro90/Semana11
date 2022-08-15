using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;

namespace ApiMusicas.Api.ViewModels;

public class AlbumArtistaViewModel
{
  public AlbumArtistaViewModel(Album album)
  {
    Id = album.Id;
    Nome = album.Nome;
    AnoLancamento = album.AnoLancamento;
    Artista = new ArtistaViewModel(album.Artista);
  }

  public int Id { get; set; }
  public string Nome { get; set; }
  public int AnoLancamento { get; set; }
  public ArtistaViewModel Artista { get; set; }
}