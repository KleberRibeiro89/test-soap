using FluentValidation;
using MiniHubApi.Application.Dtos;

namespace MiniHubApi.Application.Validators
{
    public class StatusCreditoValidator : AbstractValidator<StatusCreditoResquestDto>
    {
        public StatusCreditoValidator()
        {
            ValidateAcao();
            ValidateOpcao();
        }

        private void ValidateAcao() 
        {
            RuleFor(s => s.Acao).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode("STC-001").WithMessage("E necessario informar uma Acao")
                .MaximumLength(1).WithErrorCode("STC-002").WithMessage("O tipo do campo deve ser igual 'B'");

        }
        private void ValidateOpcao()
        {
            RuleFor(s => s.Opcao).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode("STC-001").WithMessage("E necessario informar uma Opcao")
                .MaximumLength(2).WithErrorCode("STC-002").WithMessage("O tipo do campo deve ser igual o exemplo 90 ou 92 ou 95");

        }
    }
}
