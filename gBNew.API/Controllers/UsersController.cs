using gBNew.API.DTOs;
using gBNew.API.Roles;
using gBNew.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gBNew.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }


  [HttpGet]
  public async Task<ActionResult<List<UserDTO>>> Get()
  {
    var userDto = await _userService.GetAllUsers();
    if (userDto == null)
    {
      return NotFound("Usuario não encontrado");
    }
    return Ok(userDto);
  }

  [HttpGet("{id:int}", Name = "GetUser")]
  public async Task<ActionResult<UserDTO>> Get(int id)
  {
    var userDto = await _userService.GetUserById(id);
    if (userDto == null)
    {
      return NotFound("Usuario não encontrado");
    }
    return Ok(userDto);
  }

  [HttpPost]
  public async Task<ActionResult> Post([FromBody] UserDTO userDto)
  {
    if (userDto == null)
    {
      return BadRequest("Invalid Data");
    }
    await _userService.CreateUser(userDto);
    return new CreatedAtRouteResult("GetUser", new { id = userDto.UserId }, userDto);
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> Put(int id, [FromBody] UserDTO userDto)
  {
    if (id != userDto.UserId)
    {
      return BadRequest("Invalid Data");
    }
    if(userDto == null)
    {
      return BadRequest();
    }

    await _userService.UpdateUser(userDto);
    return Ok(userDto);
  }

  [HttpDelete("{id:int}")]
  // [Authorize(Roles = Role.Admin)]
  public async Task<ActionResult> Delete(int id)
  {
    var userDto = await _userService.GetUserById(id);
    if (userDto == null)
    {
      return NotFound("User not found");
    }
    await _userService.DeleteUser(id);
    return Ok(userDto);
  }

}

