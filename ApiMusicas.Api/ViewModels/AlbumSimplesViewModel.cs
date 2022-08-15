using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;

namespace ApiMusicas.Api.ViewModels;

public class AlbumSimplesViewModel
{
  public AlbumSimplesViewModel(Album album)
  {
    Id = album.Id;
    Nome = album.Nome;
    AnoLancamento = album.AnoLancamento;
  }

  public int Id { get; set; }
  public string Nome { get; set; }
  public int AnoLancamento { get; set; }

}