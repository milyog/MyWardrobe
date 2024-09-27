using Microsoft.AspNetCore.Mvc;

namespace MyWardrobe.Controllers
{

    [Route("/error")]
    public class ErrorsController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)] // Annars fungerar inte Swagger korrekt.
        public IActionResult Error()
        {
            return Problem();
        }

    }
}
