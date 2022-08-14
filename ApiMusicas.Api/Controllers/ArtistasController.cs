using Microsoft.AspNetCore.Mvc;
using ApiMusicas.Api.Data;
using ApiMusicas.Api.Models;
using Microsoft.AspNetCore.Mvc;
// using ApiMusicas.Api.ViewModels;
// using ApiMusicas.DTOs;
// using ApiMusicas.ViewModels;

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
}