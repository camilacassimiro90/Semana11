using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.Data;
using ApiMusicas.Api.Models;
using Microsoft.AspNetCore.Mvc;
// using ApiMusicas.Api.ViewModels;
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
    public ActionResult<List<Artista>> Get(){
        return Ok(_context.Artistas.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Artista> GetPorId(
        [FromRoute] int id
    ){
        var artista = _context.Artistas.Find(id);
        
        if(artista == null) return NotFound();

        return Ok(artista);
    }

    [HttpPost]
    public ActionResult<Artista> Post(
        [FromBody] ArtistaDTO artistaDTO
    ){
        if(_context.Artistas.Any(a => a.Nome == artistaDTO.Nome)){
            return BadRequest();
        }

        var artista = new Artista{
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
    ){
        var artista = _context.Artistas.Find(id);

        if(artista == null) 
            return NotFound();

        artista.Nome = artistaDTO.Nome;
        artista.NomeArtistico = artistaDTO.NomeArtistico;
        
        _context.SaveChanges();

        return Ok(artista);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(
        [FromRoute] int id
    ){
        var artista = _context.Artistas.Find(id);

        if(artista == null) 
            return NotFound();

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

        if(artista == null) 
            return NotFound();

        var album = new Album{
            Nome = albumDTO.Nome,
            AnoLancamento = albumDTO.AnoLancamento,
            ArtistaId = artista.Id,
            Musicas = albumDTO.Musicas?.Select(musicaDTO => new Musica{
                Nome = musicaDTO.Nome,
                Duracao = musicaDTO.Duracao,
                ArtistaId = artista.Id
            }).ToList()
        };

        _context.Albuns.Add(album);
        _context.SaveChanges();
               
        return Created($"api/artistas/{id}/album");
    }

    [HttpGet("{id}/albuns")]
    public ActionResult<List<Album>> GetAlbuns(
        [FromRoute] int id    
    )
    {
        return Ok(
            _context.Albuns
          
        );
    }
}