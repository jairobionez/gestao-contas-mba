using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/gestao-contas/hello")]
    public class HelloController: ControllerBase
    {
        [HttpGet]                
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get()
        {
            return "Hello World! Tem que ter kkkkkkkkkkkkkkkk";
        }
    }
}
