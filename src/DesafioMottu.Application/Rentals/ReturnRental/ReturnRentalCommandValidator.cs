using FluentValidation;

namespace DesafioMottu.Application.Rentals.ReturnRental;

internal class ReturnRentalCommandValidator : AbstractValidator<ReturnRentalCommand>
{
    public ReturnRentalCommandValidator()
    {
        RuleFor(c => c.LocacaoId).NotEmpty();

        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}
