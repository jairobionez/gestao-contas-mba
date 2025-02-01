using AutoMapper;
using GestaoContas.Api.V1.ViewModels.Orcamento;
using GestaoContas.Shared.Data.Contexts;
using GestaoContas.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoContas.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orcamentos")]
    public class OrcamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrcamentosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentoViewModel>>> ObterTodos()
        {
            var orcamentos = await _context.Orcamentos.Include(o => o.Categoria).Include(o => o.Usuario).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<OrcamentoViewModel>>(orcamentos));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrcamentoViewModel>> ObterPorId(Guid id)
        {
            var orcamento = await _context.Orcamentos.Include(o => o.Categoria).Include(o => o.Usuario).FirstOrDefaultAsync(o => o.Id == id);
            if (orcamento == null) return NotFound("Orçamento não encontrado.");

            return Ok(_mapper.Map<OrcamentoViewModel>(orcamento));
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoViewModel>> Adicionar(OrcamentoCriarViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var orcamento = _mapper.Map<Orcamento>(model);
            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = orcamento.Id }, _mapper.Map<OrcamentoViewModel>(orcamento));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, OrcamentoEditarViewModel model)
        {
            if (id != model.Id) return BadRequest("O ID informado não corresponde ao objeto atualizado.");

            var existingOrcamento = await _context.Orcamentos.FindAsync(id);
            if (existingOrcamento == null) return NotFound("Orçamento não encontrado.");

            _mapper.Map(model, existingOrcamento);
            _context.Orcamentos.Update(existingOrcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return NotFound("Orçamento não encontrado.");

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
