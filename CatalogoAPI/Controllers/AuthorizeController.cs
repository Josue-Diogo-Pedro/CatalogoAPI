using CatalogoAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoAPI.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizeController(
        UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<string> Get() 
    { 
        return "AuthorizeController :: Acessado em : " + DateTime.Now.ToLongDateString();
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody]UsuarioDTO model)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(error => error.Errors));

        IdentityUser user = new()
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded) return BadRequest(result.Errors);

        await _signInManager.SignInAsync(user, false);
        return Ok(GeraToken(model));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody]UsuarioDTO userInfo)
    {
        //Verifica se o modelo é válido
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(error => error.Errors));

        //Verifica as credencias do usuário e retorna um valor
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, false, false);

        if (result.Succeeded) return Ok(GeraToken(userInfo));
        else
        {
            ModelState.AddModelError(string.Empty, "Login Inválido...");
            return BadRequest(ModelState);
        }
    }

    private UsuarioToken GeraToken(UsuarioDTO userInfo)
    {
        //define declarações do usuário
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("meuPet", "Pipoca"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //gera uma chave com base em um algoritmo simetrico
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //tempo de expiração do token
        var expiracao = _configuration["TokenConfiguration:ExpireHours"];
        var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

        //classe que representa um token JWT e gera um token
        JwtSecurityToken token = new(
            issuer: _configuration["TokenConfiguration:Issuer"],
            audience: _configuration["TokenConfiguration:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credenciais
        );

        //retorna os dados com o token e informações
        return new UsuarioToken()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Token JWT OK"
        };

    }
}
