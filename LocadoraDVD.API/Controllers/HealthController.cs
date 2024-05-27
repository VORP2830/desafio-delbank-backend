using Microsoft.AspNetCore.Mvc;

namespace LocadoraDVD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok(new
        {
            Message = "Welcome to LocacadoraDVD API",
            AccessDate = DateTime.Now.ToLongDateString(),
        });
    }
}
