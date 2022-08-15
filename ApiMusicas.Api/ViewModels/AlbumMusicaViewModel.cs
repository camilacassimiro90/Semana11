using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;
namespace ApiMusicas.Api.ViewModels;

public class AlbumMusicaViewModel
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public TimeSpan Duracao { get; set; }
}