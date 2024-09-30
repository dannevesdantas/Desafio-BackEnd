using FluentValidation;

namespace DesafioMottu.Application.Vehicles.RegisterVehicle;

internal class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
{
    public RegisterVehicleCommandValidator()
    {
        RuleFor(c => c.Ano)
            .NotEmpty()
            .GreaterThanOrEqualTo(1885);

        RuleFor(c => c.Modelo).NotEmpty();

        RuleFor(c => c.Placa).NotEmpty();
    }
}
