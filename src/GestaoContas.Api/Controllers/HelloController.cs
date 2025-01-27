using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.Controllers
{
    [ApiController]
    [Route("api/v1/gestao-contas/[Controller]")]
    public class HelloController: ControllerBase
    {
        [HttpGet("obter-mensagem")]                
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> Get()
        {
            return "Hello World! Tem que ter kkkkkkkkkkkkkkkk teste";
        }
    }
}
