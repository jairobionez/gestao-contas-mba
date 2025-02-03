﻿using GestaoContas.Api.Controllers;
using GestaoContas.Api.V1.ViewModels.Usuarios;
using GestaoContas.Shared.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GestaoContas.Shared.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GestaoContas.Shared.Extensions;

namespace GestaoContas.Api.V1.Controllers
{    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuariosController : MainSignInController
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        //TODO: Fazer uma controller base específica para quem utiliza UserManager e JwtSettings
        public UsuariosController(
            SignInManager<IdentityUser<Guid>> signInManager, 
            UserManager<IdentityUser<Guid>> userManager, 
            IOptions<JwtSettings> jwtSettings, 
            ApplicationDbContext context, 
            IMapper mapper)
            :base(userManager, jwtSettings) 
        {
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UsuarioViewModel>> ObterPorId(Guid id)
        {
            var usuario = _context.Usuarios.FirstOrDefaultAsync(x=>x.Id == id);
            
            return Ok(_mapper.Map<UsuarioViewModel>(await usuario));
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> ObterTodos()
        {
            var usuarios = _context.Usuarios.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<UsuarioViewModel>>(await usuarios));
        }

        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Cadastrar(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser<Guid>()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Senha!);

            await _userManager.AddClaimsAsync(user,
                    new List<Claim>() {
                        new Claim(ClaimName.Categoria, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String),
                        new Claim(ClaimName.Orcamento, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String),
                        new Claim(ClaimName.Transacao, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String)
                    });

            if (!result.Succeeded) return Problem("Falha ao cadastrar usuario", statusCode: StatusCodes.Status400BadRequest);

            var autor = new Usuario()
            {
                Nome = model.Nome,
                Id = Guid.Parse(await _userManager.GetUserIdAsync(user))
            };
            await _context.AddAsync(autor);
            await _context.SaveChangesAsync();

            await _signInManager.SignInAsync(user, false);
            return Ok(await GetJwt(model.Email!));
        }

        //[HttpDelete("{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesDefaultResponseType]
        //public async Task<ActionResult<IEnumerable<UsuarioViewModel>>> Excluir(Guid id)
        //{
        //    var usuario = await _context.Usuarios.FindAsync(id);            
        //    _context.Usuarios.Remove(usuario);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
