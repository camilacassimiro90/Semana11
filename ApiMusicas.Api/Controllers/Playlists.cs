using ApiMusicas.Api.Data;
using ApiMusicas.Api.Models;
using ApiMusicas.Api.ViewModels;
using ApiMusicas.Api.DTOs;
using ApiMusicas.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMusicasEF.Controllers;

[ApiController]
[Route("api/playlists")]
public class PlaylistsController : ControllerBase
{
  private readonly MusicasDbContext _context;

  public PlaylistsController(MusicasDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public ActionResult<List<Playlist>> Get()
  {
    return Ok(
        _context.Playlists
        .Select(p => new PlaylistViewModel(p))
        .ToList()
    );
  }

  [HttpGet("{id}")]
  public ActionResult<Playlist> GetPorId(
      [FromRoute] int id
  )
  {
    var playlist = _context.Playlists.Find(id);

    if (playlist == null) return NotFound();

    return Ok(new PlaylistViewModel(playlist));
  }

  [HttpPost]
  public ActionResult<PlaylistViewModel> Post(
      [FromBody] PlaylistDTO playlistDTO
  )
  {
    if (_context.Playlists.Any(a => a.Nome == playlistDTO.Nome))
    {
      return BadRequest(new RetornoComFalhaViewModel("Já existe uma playlist com esse nome"));
    }

    var playlist = new Playlist
    {
      Nome = playlistDTO.Nome
    };

    _context.Playlists.Add(playlist);

    _context.SaveChanges();

    return Created("api/playlists", new PlaylistViewModel(playlist));
  }

  [HttpPut("{id}")]
  public ActionResult<PlaylistViewModel> Put(
      [FromRoute] int id,
      [FromBody] PlaylistDTO playlistDTO
  )
  {
    var playlist = _context.Playlists.Find(id);

    if (playlist == null)
      return NotFound(new RetornoComFalhaViewModel("Playlist não encontrada."));

    playlist.Nome = playlistDTO.Nome;

    _context.SaveChanges();

    return Ok(new PlaylistViewModel(playlist));
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(
      [FromRoute] int id
  )
  {
    var playlist = _context.Playlists.Find(id);

    if (playlist == null)
      return NotFound(new RetornoComFalhaViewModel("Playlist não encontrada."));

    _context.Playlists.Remove(playlist);
    _context.SaveChanges();

    // 204
    return NoContent();
  }

  [HttpPost("{id}/musicas")]
  public ActionResult<MusicaCompletaViewModel> PostMusicas(
      [FromRoute] int id,
      [FromBody] PlaylistItemDTO itemDTO
  )
  {
    var playlist = _context.Playlists.Find(id);

    if (playlist == null)
      return NotFound(new RetornoComFalhaViewModel("Playlist não encontrada."));

    var musica = _context.Musicas
        .Include(m => m.Album)
        .Include(m => m.Artista)
        .Where(m => m.Id == itemDTO.MusicaId)
        .FirstOrDefault();

    if (musica == null)
      return NotFound(new RetornoComFalhaViewModel("Musica não encontrada."));

    if (_context.PlaylistMusicas.Any(pm => pm.MusicaId == musica.Id && pm.PlaylistId == playlist.Id))
    {
      return BadRequest(new RetornoComFalhaViewModel("Música já existe na playlist."));
    }

    var item = new PlaylistMusica
    {
      PlaylistId = playlist.Id,
      MusicaId = itemDTO.MusicaId
    };

    _context.PlaylistMusicas.Add(item);
    _context.SaveChanges();

    return Created($"api/playlists/{id}/musicas", new MusicaCompletaViewModel(musica));
  }

  [HttpGet("{id}/musicas")]
  public ActionResult<List<MusicaCompletaViewModel>> GetAlbuns(
      [FromRoute] int id
  )
  {
    return Ok(
        _context.PlaylistMusicas
        .Where(a => a.PlaylistId == id)
        .Include(a => a.Musica)
            .ThenInclude(m => m.Album)
        .Include(a => a.Musica)
            .ThenInclude(m => m.Artista)
        .Select(a => new MusicaCompletaViewModel(a.Musica))
        .ToList()
    );
  }
}