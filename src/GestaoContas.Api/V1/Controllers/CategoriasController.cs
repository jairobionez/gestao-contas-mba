using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.Models;
using GestaoContas.Api.V1.ViewModels.Categorias;
using GestaoContas.Shared.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GestaoContas.Shared.Domain;

namespace GestaoContas.Api.V1.Controllers
{    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categorias")]
    public class CategoriasController : MainController
    {

        private ApplicationDbContext _context;
        private IMapper _mapper;

        //private static List<CategoriaModel> _listMock = [
        //            new CategoriaModel { Id = Guid.Parse("5317b802-cf9e-4227-abd1-4f30168b4573"), Nome = "Alimentação", Ativo = true },
        //            new CategoriaModel { Id = Guid.Parse("a4a786a4-802e-4c22-9a70-a8196bf78a0a"), Nome = "Transporte", Ativo = true  },
        //            new CategoriaModel { Id = Guid.Parse("2f069932-7281-4c35-bbcf-e0ced13d2a05"), Nome = "Estudos", Ativo = true },
        //            new CategoriaModel { Id = Guid.Parse("9a579a03-fc7d-4817-b7b4-aff87b483587"), Nome = "Lazer", Ativo = true },
        //            new CategoriaModel { Id = Guid.Parse("eb77f9b0-7b5c-4b5d-b7ec-fdf5deeda6b1"), Nome = "Viagem", Ativo = true },
        //            new CategoriaModel { Id = Guid.Parse("c9bdc038-48df-4a58-abb8-ec60b7a588cf"), Nome = "Saúde", Ativo = true },
        //            new CategoriaModel { Id = Guid.Parse("4634bff9-7800-4c77-aae5-ad56797b4d07"), Nome = "Outros", Ativo = true },
        //        ];

        public CategoriasController(
            ApplicationDbContext context,
            IMapper mapper) : base()
        {
            _context = context;
            _mapper = mapper;
        }

        //public CategoriasController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        //{
        //}
        //public static List<CategoriaModel> CategoriasMock => _listMock;
        //public CategoriasController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        //{
        //}

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> Get()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _context.Categorias.ToListAsync());
            //return _listMock;
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        [AllowAnonymous]
        [HttpGet("{busca}")]
        public async Task<IEnumerable<CategoriaViewModel>> Get(string busca)
        {
            //return _listMock.Where(x => x.Nome.ToLower().Contains(busca.ToLower()) && x.Ativo);
            //Motta adicionou
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(
                await _context.Categorias
                    .Where(c => c.Nome.ToLower().Contains(busca.ToLower()) && c.Ativo)
                    .ToListAsync()
            );
            //return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoriaViewModel>> Get(Guid id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id); 

            if (categoria == null) 
                return NotFound();

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        //[ClaimsAuthorize("Categoria", "Adicionar")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoriaViewModel>> Post(CategoriaCriarViewModel categoriaModel)
        {
            if (!ModelState.IsValid) 
                return CustomResponse(ModelState);

            var categoria = _mapper.Map<Categoria>(categoriaModel);
            
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), categoria.Id, _mapper.Map<CategoriaViewModel>(categoria));
        }

        //[ClaimsAuthorize("Categoria", "Atualizar")]

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CategoriaModel>> Put(Guid id, CategoriaEditarViewModel categoriaModel)
        {
            if (id != categoriaModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(categoriaModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var categoriaExistente = await _context.Categorias.FindAsync(id);
            if (categoriaExistente == null) return NotFound();            

            categoriaExistente.Atualizar(_mapper.Map<Categoria>(categoriaModel));

            _context.Update(categoriaExistente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExiste(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        //[ClaimsAuthorize("Categoria", "Excluir")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CategoriaModel>> Delete(Guid id)
        {
            if (_context.Categorias == null) return NotFound();

            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null) return NotFound();

            //if (!categoria.PodeSermodificadoOuExcluidoPor(User)) return Unauthorized();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExiste(Guid id)
        {
            return (_context.Categorias?.Any(p => p.Id == id)).GetValueOrDefault();
        }
    }
}
