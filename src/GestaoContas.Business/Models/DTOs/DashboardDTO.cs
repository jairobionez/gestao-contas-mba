namespace GestaoContas.Business.Models.DTOs
{
    public class DashboardDTO
    {
        public DashboardDTO() 
        {

        }

        public decimal TotalEntradas { get; set; }
        public decimal TotalSaidas { get; set; }
        public decimal MediaGastosDiarios { get; set; }
        public List<CategoriaGastoDTO>? CategoriasGasto { get; set; }
    }

    public class CategoriaGastoDTO
    {
        public CategoriaGastoDTO(string nomeCategoria, decimal totalGasto)
        {
            NomeCategoria = nomeCategoria;
            TotalGasto = totalGasto;
        }

        public string NomeCategoria { get; set; }
        public decimal TotalGasto { get; set; }
    }
}
