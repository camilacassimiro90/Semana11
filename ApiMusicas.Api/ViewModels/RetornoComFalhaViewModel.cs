
namespace ApiMusicas.Api.ViewModels;

public class RetornoComFalhaViewModel
{
  public string Falha { get; set; }

  public RetornoComFalhaViewModel(string falha)
  {
    this.Falha = falha;
  }
}