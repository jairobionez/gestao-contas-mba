﻿namespace GestaoContas.Api.V1.ViewModels.Orcamento
{
    public class OrcamentoEditarViewModel
    {
        public Guid Id { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal Limite { get; set; }
        public Guid? CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
