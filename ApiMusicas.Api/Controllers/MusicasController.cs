using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.Data;
using ApiMusicas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.ViewModels;
using ApiMusicas.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiMusicas.Api.Controllers;

[ApiController]
[Route("api/musicas")]
public class MusicasController : ControllerBase
{
  private readonly MusicasDbContext _context;

  public MusicasController(MusicasDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<List<MusicaCompletaViewModel>> Get()
  {
    return Ok(
        _context.Musicas
        .Include(m => m.Album)
        .Include(m => m.Artista)
        .Select(m => new MusicaCompletaViewModel(m))
        .ToList()
    );
  }

  [HttpGet("{id}")]
  public ActionResult<MusicaCompletaViewModel> GetPorId(
      [FromRoute] int id
  )
  {
    var musica = _context.Musicas
        .Include(m => m.Album)
        .Include(m => m.Artista)
        .Where(m => m.Id == id)
        .Select(m => new MusicaCompletaViewModel(m))
        .FirstOrDefault();


    if (musica == null) return NotFound();

    return Ok(musica);
  }

  [HttpPost]
  public ActionResult<MusicaCompletaViewModel> Post(
      [FromBody] CriacaoMusicaDTO musicaDTO
  )
  {
    if (!_context.Artistas.Any(a => a.Id == musicaDTO.ArtistaId))
      return NotFound(new RetornoComFalhaViewModel("Artista não encontrado."));

    if (musicaDTO.AlbumId.HasValue && !_context.Albuns.Any(a => a.Id == musicaDTO.AlbumId))
      return NotFound(new RetornoComFalhaViewModel("Album não encontrado."));

    var musica = new Musica
    {
      Nome = musicaDTO.Nome,
      AlbumId = musicaDTO.AlbumId,
      ArtistaId = musicaDTO.ArtistaId,
      Duracao = musicaDTO.Duracao
    };

    _context.Musicas.Add(musica);

    _context.SaveChanges();

    var retorno = _context.Musicas
        .Include(m => m.Album)
        .Include(m => m.Artista)
        .Where(m => m.Id == musica.Id)
        .Select(m => new MusicaCompletaViewModel(m))
        .FirstOrDefault();

    return Created("api/musicas", retorno);
  }

  [HttpPut("{id}")]
  public ActionResult<MusicaCompletaViewModel> Put(
      [FromRoute] int id,
      [FromBody] EdicaoMusicaDTO musicaDTO
  )
  {
    var musica = _context.Musicas
        .Include(m => m.Album)
        .Include(m => m.Artista)
        .Where(m => m.Id == id)
        .FirstOrDefault();

    if (musica == null)
      return NotFound(new RetornoComFalhaViewModel("Musica não encontrada."));

    musica.Nome = musicaDTO.Nome;
    musica.Duracao = musicaDTO.Duracao;

    _context.SaveChanges();

    return Ok(new MusicaCompletaViewModel(musica));
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(
      [FromRoute] int id
  )
  {
    var musica = _context.Musicas.Find(id);

    if (musica == null)
      return NotFound(new RetornoComFalhaViewModel("Musica não encontrado."));

    _context.Musicas.Remove(musica);
    _context.SaveChanges();

    // 204
    return NoContent();
  }
}