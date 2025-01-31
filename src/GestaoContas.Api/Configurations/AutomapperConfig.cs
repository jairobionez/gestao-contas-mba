using AutoMapper;
using GestaoContas.Api.V1.ViewModels.Categorias;
using GestaoContas.Api.V1.ViewModels.Usuarios;
using GestaoContas.Shared.Domain;

namespace GestaoContas.Api.Configurations
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario,UsuarioViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaViewModel>().ForMember(c => c.Ativo, ca => ca.MapFrom(x => x.Padrao)).ReverseMap();
            CreateMap<Categoria, CategoriaCriarViewModel>().ReverseMap();
            CreateMap<Categoria, CategoriaEditarViewModel>().ReverseMap();
        }
    }
}
