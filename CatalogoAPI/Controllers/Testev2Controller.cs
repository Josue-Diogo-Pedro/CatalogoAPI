using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers;

[ApiVersion("2.0")]
[Route("api/{v:apiVersion}/teste")]
[ApiController]
public class Testev2Controller : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Content("<html><body><h2>Testev2Controller - V 1.0</h2></body></html>");
    }
}
