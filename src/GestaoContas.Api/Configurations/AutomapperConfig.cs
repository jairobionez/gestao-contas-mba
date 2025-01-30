using AutoMapper;
using GestaoContas.Api.V1.ViewModels.Usuarios;
using GestaoContas.Shared.Domain;

namespace GestaoContas.Api.Configurations
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario,UsuarioViewModel>().ReverseMap();
        }
    }
}
