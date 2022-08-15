using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.Data;
using ApiMusicas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.ViewModels;
using ApiMusicas.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiMusicas.Api.Controllers;

[ApiController]
[Route("api/artistas")]
public class ArtistasController : ControllerBase
{
  private readonly MusicasDbContext _context;

  public ArtistasController(MusicasDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<List<Artista>> Get()
  {
    return Ok(_context.Artistas.ToList());
  }

  [HttpGet("{id}")]
  public ActionResult<Artista> GetPorId(
      [FromRoute] int id
  )
  {
    var artista = _context.Artistas.Find(id);

    if (artista == null) return NotFound();

    return Ok(artista);
  }

  [HttpPost]
  public ActionResult<Artista> Post(
      [FromBody] ArtistaDTO artistaDTO
  )
  {
    if (_context.Artistas.Any(a => a.Nome == artistaDTO.Nome))
    {
      return BadRequest(new RetornoComFalhaViewModel("Já existe um artista com esse nome"));
    }

    var artista = new Artista
    {
      Nome = artistaDTO.Nome,
      NomeArtistico = artistaDTO.NomeArtistico,
      
    };

    _context.Artistas.Add(artista);

    _context.SaveChanges();

    return Created("api/artistas", artista);
  }

  [HttpPut("{id}")]
  public ActionResult<Artista> Put(
      [FromRoute] int id,
      [FromBody] ArtistaDTO artistaDTO
  )
  {
    var artista = _context.Artistas.Find(id);

    if (artista == null)
      return NotFound(new RetornoComFalhaViewModel("Artista não encontrado."));

    artista.Nome = artistaDTO.Nome;
    artista.NomeArtistico = artistaDTO.NomeArtistico;
    

    _context.SaveChanges();

    return Ok(artista);
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(
      [FromRoute] int id
  )
  {
    var artista = _context.Artistas.Find(id);

    if (artista == null)
      return NotFound(new RetornoComFalhaViewModel("Artista não encontrado."));

    _context.Artistas.Remove(artista);
    _context.SaveChanges();

    // 204
    return NoContent();
  }

  [HttpPost("{id}/albuns")]
  public ActionResult<Album> PostAlbum(
      [FromRoute] int id,
      [FromBody] ArtistaAlbumDTO albumDTO
  )
  {
    var artista = _context.Artistas.Find(id);

    if (artista == null)
      return NotFound(new RetornoComFalhaViewModel("Artista não encontrado."));

    var album = new Album
    {
      Nome = albumDTO.Nome,
      AnoLancamento = albumDTO.AnoLancamento,
      ArtistaId = artista.Id,
      Musicas = albumDTO.Musicas?.Select(musicaDTO => new Musica
      {
        Nome = musicaDTO.Nome,
        Duracao = musicaDTO.Duracao,
        ArtistaId = artista.Id
      }).ToList()
    };

    _context.Albuns.Add(album);
    _context.SaveChanges();

    var viewModel = new AlbumComMusicasViewModel(album);

    return Created($"api/artistas/{id}/album", viewModel);
  }

  [HttpGet("{id}/albuns")]
  public ActionResult<List<AlbumComMusicasViewModel>> GetAlbuns(
      [FromRoute] int id
  )
  {
    return Ok(
        _context.Albuns
        .Where(a => a.ArtistaId == id)
        .Include(a => a.Musicas)
        .Select(a => new AlbumComMusicasViewModel(a))
        .ToList()
    );
  }
}