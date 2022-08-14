namespace ApiMusicas.Api.Models;

public class Album
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int AnoLancamento { get; set; }
    public int ArtistaId { get; set; }
    public Artista Artista { get; set; }
    public List<Musica> Musicas { get; set; }

    public Album(string nome, int anoLancamento, Artista artista)
    {
        Nome = nome;
        AnoLancamento = anoLancamento;
        Artista = artista;
    }

    public Album()
    {

    }
}