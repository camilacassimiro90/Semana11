namespace ApiMusicas.Api.Models;

public class Album
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int Ano { get; set; }
    public int ArtistaId { get; set; }
    public Artista Artista { get; set; }
    public List<Musica> Musicas { get; set; }

    public Album(string nome, int ano, Artista artista)
    {
        Nome = nome;
        Ano = ano;
        Artista = artista;
    }

    public Album()
    {

    }
}