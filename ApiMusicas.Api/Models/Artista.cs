namespace ApiMusicas.Api.Models;

public class Artista
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeArtistico { get; set; }
    public string PaisOrigem { get; set; }

    public List<Musica> Musicas { internal get; set; }
    public List<Album> Albuns { internal get; set; }

    public Artista(string nome, string nomeArtistico, string paisOrigem)
    {
        Nome = nome;
        NomeArtistico = nomeArtistico;
        PaisOrigem = paisOrigem;
    }

    public Artista()
    {

    }

}