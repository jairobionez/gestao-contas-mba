using AutoMapper;
using GestaoContas.Api.V1.ViewModels.Categorias;
using GestaoContas.Api.V1.ViewModels.Orcamento;
using GestaoContas.Api.V1.ViewModels.Transacao;
using GestaoContas.Api.V1.ViewModels.Usuarios;
using GestaoContas.Api.V2.ViewModels.Categorias;
using GestaoContas.Api.V2.ViewModels.Transacoes;
using GestaoContas.Api.V2.ViewModels.Usuarios;
using GestaoContas.Business.Models;

namespace GestaoContas.Api.Configurations
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            //V1
            CreateMap<Usuario,UsuarioViewModel>().ReverseMap();
            CreateMap<Categoria, V1.ViewModels.Categorias.CategoriaViewModel>().ForMember(c => c.Ativo, ca => ca.MapFrom(x => x.Padrao)).ReverseMap();
            CreateMap<Categoria, CategoriaCriarViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaEditarViewModel>().ReverseMap();
            CreateMap<Transacao, V1.ViewModels.Transacao.TransacaoViewModel>().ReverseMap();
            CreateMap<Orcamento, OrcamentoViewModel>()
            .ForMember(dest => dest.CategoriaNome, opt => opt.MapFrom(src => src.Categoria!.Nome))
            .ForMember(dest => dest.UsuarioNome, opt => opt.MapFrom(src => src.Usuario!.Nome));

            CreateMap<OrcamentoCriarViewModel, Orcamento>();
            CreateMap<OrcamentoEditarViewModel, Orcamento>();


            //V2           
            CreateMap<Usuario, UsuarioListarViewModel>();
            CreateMap<Categoria, V2.ViewModels.Categorias.CategoriaViewModel>();
            CreateMap<CategoriaCadastrarViewModel, Categoria>();
            CreateMap<CategoriaAtualizarViewModel, Categoria>();
            CreateMap<Transacao, V2.ViewModels.Transacoes.TransacaoViewModel>();
            CreateMap<TransacaoCadastrarViewModel, Transacao>();
            CreateMap<TransacaoAtualizarViewModel, Transacao>();
        }
    }
}
