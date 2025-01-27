using GestaoContas.Api.Controllers;
using GestaoContas.Api.Models;
using GestaoContas.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categorias")]
    public class CategoriasController : MainController
    {
        private static List<CategoriaModel> _listMock = [
                    new CategoriaModel { Id = Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573"), Nome = "Alimentação", Ativo = true },
                    new CategoriaModel { Id = Guid.Parse("a4a786a4-802e-4c22-9a70-a8196bf78a0a"), Nome = "Transporte", Ativo = true  },
                    new CategoriaModel { Id = Guid.Parse("2f069932-7281-4c35-bbcf-e0ced13d2a05"), Nome = "Estudos", Ativo = true },
                    new CategoriaModel { Id = Guid.Parse("9a579a03-fc7d-4817-b7b4-aff87b483587"), Nome = "Lazer", Ativo = true },
                    new CategoriaModel { Id = Guid.Parse("eb77f9b0-7b5c-4b5d-b7ec-fdf5deeda6b1"), Nome = "Viagem", Ativo = true },
                    new CategoriaModel { Id = Guid.Parse("c9bdc038-48df-4a58-abb8-ec60b7a588cf"), Nome = "Saúde", Ativo = true },
                    new CategoriaModel { Id = Guid.Parse("4634bff9-7800-4c77-aae5-ad56797b4d07"), Nome = "Outros", Ativo = true },
                ];

        public static List<CategoriaModel> CategoriasMock => _listMock;

        public CategoriasController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        {
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<CategoriaModel>> ObterTodos()
        {
            return _listMock;
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        [AllowAnonymous]
        [HttpGet("{busca}")]
        public async Task<IEnumerable<CategoriaModel>> Obter(string busca)
        {
            return _listMock.Where(x => x.Nome.ToLower().Contains(busca.ToLower()) && x.Ativo);
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaModel>> ObterPorId(Guid id)
        {
            var categoria = _listMock.FirstOrDefault(x => x.Id == id);

            if (categoria == null) 
                return NotFound();

            return categoria;
        }

        //[ClaimsAuthorize("Categoria", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> Adicionar(CategoriaModel categoriaModel)
        {
            if (!ModelState.IsValid) 
                return CustomResponse(ModelState);

            var novaCategoria = new CategoriaModel
            {
                Id = Guid.NewGuid(),
                Nome = categoriaModel.Nome,
                Ativo = categoriaModel.Ativo,
            };
            _listMock.Add(novaCategoria);

            return CustomResponse(novaCategoria);
        }

        //[ClaimsAuthorize("Categoria", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoriaModel>> Atualizar(Guid id, CategoriaModel categoriaModel)
        {
            if (id != categoriaModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(categoriaModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoriaIndex = _listMock.FindIndex(x => x.Id == categoriaModel.Id);
            _listMock[categoriaIndex].Nome = categoriaModel.Nome;
            _listMock[categoriaIndex].Ativo = categoriaModel.Ativo;

            return CustomResponse(categoriaModel);
        }

        //[ClaimsAuthorize("Categoria", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CategoriaModel>> Excluir(Guid id)
        {
            var categoria = _listMock.FirstOrDefault(x => x.Id == id);

            if (categoria == null) 
                return NotFound();

            _listMock.Remove(categoria);    

            return CustomResponse(categoria);
        }
    }
}
