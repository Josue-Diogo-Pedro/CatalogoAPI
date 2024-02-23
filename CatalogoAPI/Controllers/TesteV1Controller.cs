using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[ApiVersion("1.0")]
[Route("api/{v:apiVersion}/teste")]
[ApiController]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<html><body><h2>Testev1Controller - V 1.0</h2></body></html>");
    }
}
