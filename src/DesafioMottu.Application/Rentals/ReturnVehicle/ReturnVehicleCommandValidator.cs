using FluentValidation;

namespace DesafioMottu.Application.Rentals.ReturnVehicle;

internal class ReturnVehicleCommandValidator : AbstractValidator<ReturnVehicleCommand>
{
    public ReturnVehicleCommandValidator()
    {
        RuleFor(c => c.RentId).NotEmpty();

        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}
