using Newtonsoft.Json;

namespace CatalogoAPI.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Trace { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
