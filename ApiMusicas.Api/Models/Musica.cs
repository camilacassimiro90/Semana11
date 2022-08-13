namespace ApiMusicas.Api.Models;

public class Musica
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Duracao { get; set; }
    public int ArtistaId { get; set; }
    public int AlbumId { get; set; }
    public Artista Artista { get; set; }
    public Album Album { internal get; set; }

    public Musica(string nome, double duracao, Artista artista, Album album)
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