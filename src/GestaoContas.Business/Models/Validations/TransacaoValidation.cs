using FluentValidation;

namespace GestaoContas.Business.Models.Validations
{
    public class TransacaoValidation :AbstractValidator<Transacao>
    {
        public TransacaoValidation()
        {
            RuleFor(t => t.Valor)                
                .GreaterThanOrEqualTo(0).WithMessage("O campo {PropertyName} não pode ser negativo.");
            RuleFor(t => t.TipoTransacao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado.");
            RuleFor(t=>t.Data)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado.");
            RuleFor(t => t.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado.")
                .MaximumLength(255).WithMessage("O campo {PropertyName} pode ter no máximo {MaxLength} caracteres.");
        }
    }
}
