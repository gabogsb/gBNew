using System.ComponentModel.DataAnnotations;
using gBNew.API.Models;

namespace gBNew.API.DTOs;
public class UserDTO
{
  [Key]
  public int UserId { get; set; }

  [StringLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
  [Required(ErrorMessage = "Informe o seu username")]
  public string? Username { get; set; }

  [StringLength(100, ErrorMessage = "O tamanho máximo é de 100 caracteres")]
  [Required(ErrorMessage = "Informe o seu nome")]
  public string? Name { get; set; }

  [StringLength(70, ErrorMessage = "O tamanho máximo é de 70 caracteres")]
  [Required(ErrorMessage = "Informe o seu e-mail")]
  public string? Email { get; set; }

  [Required(ErrorMessage = "Informe a sua senha")]
  public string? Password { get; set; }
  public string? Avatar { get; set; }
}
