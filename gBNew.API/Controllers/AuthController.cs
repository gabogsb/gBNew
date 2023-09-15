using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gBNew.API.DTOs;
using gBNew.API.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace gBNew.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;
  private readonly IConfiguration _configuration;

  public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _configuration = configuration;
  }

  [HttpGet]
  public  ActionResult<string> Get()
  {
    return "AuthController :: Acessado em " + DateTime.Now.ToLongDateString();
  }


  [HttpPost("register")]
  public async Task<ActionResult> RegisterUser([FromBody]AuthDTO authDto)
  {
    // if(!ModelState.IsValid)
    // {
    //   return BadRequest(ModelState.Values.Select(e => e.Errors));
    // }

    var user = new IdentityUser
    {
      UserName = authDto.Username,
      Email = authDto.Username,
      EmailConfirmed = true,
    };

    var result = await _userManager.CreateAsync(user, authDto.Password);

    if (!result.Succeeded)
    {
      return BadRequest(result.Errors);
    }

    await _signInManager.SignInAsync(user, false);
    return Ok(GetToken(authDto));
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login([FromBody]AuthDTO authDto)
  {
    var result = await _signInManager.PasswordSignInAsync(authDto.Username, authDto.Password, isPersistent: false, lockoutOnFailure: false);
    if(result.Succeeded)
    {
      return Ok(GetToken(authDto));
    }
    else
    {
      ModelState.AddModelError(string.Empty, "Login Invalid");
      return BadRequest(ModelState);
    }
  }


  private AuthToken GetToken(AuthDTO authDto)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.UniqueName, authDto.Username),
      new Claim("Salve", "Hello"),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var expire = _configuration["TokenConfiguration:ExpireHours"];
    var expiration = DateTime.UtcNow.AddHours(double.Parse(expire));

    JwtSecurityToken token = new JwtSecurityToken(
      issuer: _configuration["TokenConfiguration:Issuer"],
      audience: _configuration["TokenConfiguration:Audience"],
      claims: claims,
      expires: expiration,
      signingCredentials: credentials
    );


    return new AuthToken()
    {
      Authenticated = true,
      Token = new JwtSecurityTokenHandler().WriteToken(token),
      Expiration = expiration,
      Message = "Token JWT OK"
    };

  }

}
