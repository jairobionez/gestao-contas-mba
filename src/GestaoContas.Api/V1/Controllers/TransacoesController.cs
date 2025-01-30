using GestaoContas.Api.Controllers;
using GestaoContas.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GestaoContas.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transacoes")]
    public class TransacoesController : MainController
    {

        private static List<TransacaoModel> _listMock = [
            new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Almoço 25/01/2025", Tipo = TipoTranscao.Saida, Valor = 50, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573")) },
            new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Café 25/01/2025", Tipo = TipoTranscao.Saida, Valor = 20, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573")) },
            new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Parcela MBA", Tipo = TipoTranscao.Saida, Valor = 400, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("2f069932-7281-4c35-bbcf-e0ced13d2a05")) },
            new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Plano Saúde", Tipo = TipoTranscao.Saida, Valor = 300.50M, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("c9bdc038-48df-4a58-abb8-ec60b7a588cf")) },
            new TransacaoModel { Data = DateTime.Now, Id = Guid.NewGuid(), Descricao = "Salário", Tipo = TipoTranscao.Entrada, Valor = 4000.50M, Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == Guid.Parse("4634bff9-7800-4c77-aae5-ad56797b4d07")) },
                ];

        public TransacoesController(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings) : base(userManager, jwtSettings)
        {
        }

        //public TransacoesController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        //{
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<TransacaoModel>> ObterTodos()
        {
            return _listMock;
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        //[AllowAnonymous]
        //[HttpGet("{data:datetime?}/{categoriaId:guid?}/{tipo}")]
        //public async Task<IEnumerable<TransacaoModel>> Obter(DateTime? data, Guid? categoriaId, TipoTranscao? tipo)
        //{
        //    return _listMock;
        //    //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        //}

        [AllowAnonymous]
        [HttpPost("busca")]
        public async Task<IEnumerable<TransacaoModel>> Busca(FiltroTransacaoModel busca)
        {
            return _listMock;
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TransacaoModel>> ObterPorId(Guid id)
        {
            var transacao = _listMock.FirstOrDefault(x => x.Id == id);

            if (transacao == null)
                return NotFound();

            return transacao;
        }

        //[ClaimsAuthorize("Categoria", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<TransacaoModel>> Adicionar(AdicionarTransacaoModel transacaoModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var novaTransacao = new TransacaoModel
            {
                Id = Guid.NewGuid(),
                Data = transacaoModel.Data,
                Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == transacaoModel.CategoriaId),
                Descricao = transacaoModel.Descricao,
                Tipo = transacaoModel.Tipo,
                Valor = transacaoModel.Valor,
            };
            _listMock.Add(novaTransacao);

            return CustomResponse(novaTransacao);
        }

        //[ClaimsAuthorize("Categoria", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TransacaoModel>> Atualizar(Guid id, AdicionarTransacaoModel transacaoModel)
        {
            //if (id != transacaoModel.Id)
            //{
            //    NotificarErro("O id informado não é o mesmo que foi passado na query");
            //    return CustomResponse(transacaoModel);
            //}

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var transacaoIndex = _listMock.FindIndex(x => x.Id == id);
            _listMock[transacaoIndex].Data = transacaoModel.Data;
            _listMock[transacaoIndex].Valor = transacaoModel.Valor;
            _listMock[transacaoIndex].Descricao = transacaoModel.Descricao;
            _listMock[transacaoIndex].Tipo = transacaoModel.Tipo;
            _listMock[transacaoIndex].Categoria = CategoriasController.CategoriasMock.FirstOrDefault(x => x.Id == transacaoModel.CategoriaId);

            return CustomResponse(transacaoModel);
        }

        //[ClaimsAuthorize("Categoria", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<TransacaoModel>> Excluir(Guid id)
        {
            var transacao = _listMock.FirstOrDefault(x => x.Id == id);

            if (transacao == null)
                return NotFound();

            _listMock.Remove(transacao);

            return CustomResponse(transacao);
        }

        [AllowAnonymous]
        [HttpGet("{dataInicial:datetime}/{dataFinal:datetime}")]
        public async Task<ResumoTranscoesModel> Resumo(DateTime dataInicial, DateTime dataFinal)
        {
            return new ResumoTranscoesModel
            {
                Despesas  = 3000M,
                Receitas = 4000M,
                Saldo = 4000M - 3000M
            };
        }

    }
}
