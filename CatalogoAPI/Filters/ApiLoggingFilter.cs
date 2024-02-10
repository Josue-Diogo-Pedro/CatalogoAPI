using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoAPI.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    //executa antes da action
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("### Executando -> OnActionExecuting");
        _logger.LogInformation("####################################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        _logger.LogInformation("####################################################");
    }

    //executa depois da action
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("### Executando -> OnActionExecuted");
        _logger.LogInformation("####################################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation("####################################################");
    }
}
