using ApiMusicas.Api.Models;
namespace ApiMusicas.Api.ViewModels;

public class ArtistaViewModel
{
  public ArtistaViewModel(Artista artista)
  {
    Id = artista.Id;
    Nome = artista.Nome;
    NomeArtistico = artista.NomeArtistico;
  }

  public int Id { get; set; }
  public string Nome { get; set; }
  public string NomeArtistico { get; set; }

}
