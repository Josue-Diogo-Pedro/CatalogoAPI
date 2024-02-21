using CatalogoAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[Route("api/Ccontroller]")]
[ApiController]
public class AuthorizeController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthorizeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public string Get() => "AuthorizeController :: Acessado em : " + DateTime.Now.ToLongDateString();

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
        return Ok();
    }
}
