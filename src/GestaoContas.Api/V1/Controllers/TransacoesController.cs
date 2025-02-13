using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.V1.ViewModels.Transacao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoContas.Api.Extensions.Authorizations;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using GestaoContas.Business.Interfaces;
using Asp.Versioning;

namespace GestaoContas.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/transacoes")]
    public class TransacoesController : MainController
    {
        private ApplicationDbContext _context;        

        //private static List<TransacaoModel> _listMock = [
        //    new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Almoço 25/01/2025", Tipo = TipoTransacao.Saida, Valor = 50, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573")) },
        //    new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Café 25/01/2025", Tipo = TipoTransacao.Saida, Valor = 20, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573")) },
        //    new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Parcela MBA", Tipo = TipoTransacao.Saida, Valor = 400, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("2f069932-7281-4c35-bbcf-e0ced13d2a05")) },
        //    new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Plano Saúde", Tipo = TipoTransacao.Saida, Valor = 300.50M, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("c9bdc038-48df-4a58-abb8-ec60b7a588cf")) },
        //    new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Salário", Tipo = TipoTransacao.Entrada, Valor = 4000.50M, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("4634bff9-7800-4c77-aae5-ad56797b4d07")) },
        //        ];

        public TransacoesController(
            ApplicationDbContext context,
            IMapper mapper,
            INotificador notificador,
            IUser appUser) 
            : base(notificador, mapper, appUser)
        {
            _context = context;            
        }

        //public TransacoesController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        //{
        //}

        
        [HttpGet]
        public async Task<IEnumerable<TransacaoViewModel>> Get()
        {
            //return _listMock;
            return _mapper.Map<IEnumerable<TransacaoViewModel>>(await _context.Transacoes.ToListAsync());
        }

        [HttpGet("filtro")]
        public async Task<IEnumerable<TransacaoViewModel>> Get(DateTime? data, Guid? categoriaId, TipoTransacao? tipo)
        {
            var query = _context.Transacoes.AsQueryable();

            if (data.HasValue)
                query = query.Where(t => t.Data.HasValue && t.Data.Value.Date == data.Value.Date);

            if (categoriaId.HasValue)
                query = query.Where(t => t.CategoriaId == categoriaId.Value);

            if (tipo.HasValue)
                query = query.Where(t => t.TipoTransacao == (TipoTransacao)tipo.Value);

            var transacoes = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TransacaoViewModel>>(transacoes);
        }

        
        [HttpPost("busca")]
        public async Task<IEnumerable<TransacaoViewModel>> Get(FiltroTransacaoViewModel busca)
        {
            var query = _context.Transacoes.AsQueryable();
            if (busca.DataInicial.HasValue)
                query = query.Where(t => t.Data.HasValue && t.Data.Value.Date >= busca.DataInicial.Value.Date);

            if (busca.DataFinal.HasValue)
                query = query.Where(t => t.Data.HasValue && t.Data.Value.Date <= busca.DataFinal.Value.Date);

            if (busca.ValorInicial.HasValue)
                query = query.Where(t => t.Valor >= busca.ValorInicial.Value);

            if (busca.ValorFinal.HasValue)
                query = query.Where(t => t.Valor <= busca.ValorFinal.Value);

            if (busca.CategoriaId.HasValue)
                query = query.Where(t => t.CategoriaId == busca.CategoriaId.Value);

            if (busca.Tipo.HasValue)
                query = query.Where(t => t.TipoTransacao == (TipoTransacao)busca.Tipo.Value);

            var transacoes = await query.ToListAsync();
            return _mapper.Map<IEnumerable<TransacaoViewModel>>(transacoes);
        }

        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TransacaoViewModel>> Get(Guid id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);

            if (transacao == null)
                return NotFound();

            return _mapper.Map<TransacaoViewModel>(transacao);
        }

        [ClaimsAuthorize(ClaimName.Transacao, ClaimValue.Cadastrar)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TransacaoViewModel>> Adicionar(TransacaoViewModel transacaoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novaTransacao = _mapper.Map<Transacao>(transacaoViewModel);
            novaTransacao.Id = Guid.NewGuid(); 

            _context.Transacoes.Add(novaTransacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = novaTransacao.Id },
                _mapper.Map<TransacaoViewModel>(novaTransacao));
        }

        [ClaimsAuthorize(ClaimName.Transacao, ClaimValue.Editar)]
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TransacaoViewModel>> Atualizar(Guid id, TransacaoViewModel transacaoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transacaoExistente = await _context.Transacoes.FindAsync(id);

            if (transacaoExistente == null)
                return NotFound("Transação não encontrada.");

            _mapper.Map(transacaoViewModel, transacaoExistente);

            _context.Transacoes.Update(transacaoExistente);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<TransacaoViewModel>(transacaoExistente));
        }

        [ClaimsAuthorize(ClaimName.Transacao, ClaimValue.Excluir)]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TransacaoViewModel>> Excluir(Guid id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);

            if (transacao == null)
                return NotFound("Transação não encontrada.");

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<TransacaoViewModel>(transacao));
        }

        
        [HttpGet("resumo/{dataInicial:datetime}/{dataFinal:datetime}")]
        public async Task<ActionResult<ResumoTransacaoViewModel>> Resumo(DateTime dataInicial, DateTime dataFinal)
        {
            var transacoes = await _context.Transacoes
                .Where(t => t.Data >= dataInicial && t.Data <= dataFinal)
                .ToListAsync();

            decimal receitas = transacoes
                .Where(t => t.TipoTransacao == TipoTransacao.Entrada)
                .Sum(t => t.Valor);

            decimal despesas = transacoes
                .Where(t => t.TipoTransacao == TipoTransacao.Saida)
                .Sum(t => t.Valor);

            var resumo = new ResumoTransacaoViewModel
            {
                Despesas = despesas,
                Receitas = receitas,
                Saldo = receitas - despesas
            };

            return Ok(resumo);
        }
    }
}
