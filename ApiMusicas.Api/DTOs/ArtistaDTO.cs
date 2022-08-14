using System.ComponentModel.DataAnnotations;
namespace ApiMusicas.Api.DTOs;

public class ArtistaDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    public string NomeArtistico { get; set; }

}