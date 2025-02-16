using FluentValidation;

namespace GestaoContas.Business.Models.Validations
{
    public class OrcamentoValidation : AbstractValidator<Orcamento>
    {
        public OrcamentoValidation()
        {
            RuleFor(o => o.Limite)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que zero");

        }
    }
}
