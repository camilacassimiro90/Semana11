using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;

namespace ApiMusicas.Api.ViewModels;
public class AlbumComMusicasViewModel
{
  public AlbumComMusicasViewModel(Album album)
  {
    Id = album.Id;
    Nome = album.Nome;
    AnoLancamento = album.AnoLancamento;
    Musicas = album?.Musicas.Select(musica => new AlbumMusicaViewModel
    {
      Id = musica.Id,
      Nome = musica.Nome,
      Duracao = musica.Duracao
    }).ToList();
  }

  public int Id { get; set; }
  public string Nome { get; set; }
  public int AnoLancamento { get; set; }
  public List<AlbumMusicaViewModel> Musicas { get; set; }
}