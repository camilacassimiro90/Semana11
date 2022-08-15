namespace ApiMusicas.Api.Models;

public class Musica
{
  public int Id { get; set; }
  public string Nome { get; set; }
  public TimeSpan Duracao { get; set; }

  public int? AlbumId { get; set; }
  public int ArtistaId { get; set; }
  public virtual Artista Artista { get; set; }
  public virtual Album Album { get; set; }

  public Musica(string nome, TimeSpan duracao, Artista artista, Album album)
  {
    Nome = nome;
    Duracao = duracao;
    Artista = artista;
    Album = album;
  }

  public Musica()
  {

  }
}