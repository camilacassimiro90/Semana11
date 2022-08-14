using System.ComponentModel.DataAnnotations;
namespace ApiMusicas.Api.DTOs;

public class MusicaAlbumDTO
{

    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    public TimeSpan Duracao { get; set; }
}
