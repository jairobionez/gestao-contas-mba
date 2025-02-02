using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.Extensions.Authorizations;
using GestaoContas.Api.V1.ViewModels.Orcamento;
using GestaoContas.Shared.Data.Contexts;
using GestaoContas.Shared.Domain;
using GestaoContas.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GestaoContas.Api.V1.Controllers
{    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orcamentos")]
    public class OrcamentosController : MainController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrcamentosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentoViewModel>>> Get()
        {
            var orcamentos = await _context.Orcamentos.Include(o => o.Categoria).Include(o => o.Usuario).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<OrcamentoViewModel>>(orcamentos));
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrcamentoViewModel>> Get(Guid id)
        {
            var orcamento = await _context.Orcamentos.Include(o => o.Categoria).Include(o => o.Usuario).FirstOrDefaultAsync(o => o.Id == id);
            if (orcamento == null) return NotFound("Orçamento não encontrado.");

            return Ok(_mapper.Map<OrcamentoViewModel>(orcamento));
        }

        [ClaimsAuthorize(ClaimName.Orcamento, ClaimValue.Cadastrar)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrcamentoViewModel>> Put(OrcamentoCriarViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orcamento = _mapper.Map<Orcamento>(model);
            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = orcamento.Id }, _mapper.Map<OrcamentoViewModel>(orcamento));
        }

        [ClaimsAuthorize(ClaimName.Orcamento, ClaimValue.Editar)]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(Guid id, OrcamentoEditarViewModel model)
        {
            if (id != model.Id) return BadRequest("O ID informado não corresponde ao objeto atualizado.");

            var existingOrcamento = await _context.Orcamentos.FindAsync(id);
            if (existingOrcamento == null) return NotFound("Orçamento não encontrado.");

            _mapper.Map(model, existingOrcamento);
            _context.Orcamentos.Update(existingOrcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [ClaimsAuthorize(ClaimName.Orcamento, ClaimValue.Excluir)]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return NotFound("Orçamento não encontrado.");

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
