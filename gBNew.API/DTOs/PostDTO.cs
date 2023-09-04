using System.ComponentModel.DataAnnotations;
using gBNew.API.Models;

namespace gBNew.API.DTOs;

public class PostDTO
{
  [Key]
  public int PostId { get; set; }
  [StringLength(140, ErrorMessage = "O tamanho máximo é de 140 caracteres")]
  [Required(ErrorMessage = "Informe o nome da categoria")]
  public string? Title { get; set; }
  [StringLength(400, ErrorMessage = "O tamanho máximo é de 400 caracteres")]
  [Required(ErrorMessage = "O corpo da postagem não pode ficar vazio")]
  public string? Body { get; set; }
}
