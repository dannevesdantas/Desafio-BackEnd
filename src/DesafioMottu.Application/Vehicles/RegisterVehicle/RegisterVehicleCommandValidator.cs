using FluentValidation;

namespace DesafioMottu.Application.Motorcycles.RegisterVehicle;

internal class RegisterVehicleCommandValidator : AbstractValidator<RegisterVehicleCommand>
{
    public RegisterVehicleCommandValidator()
    {
        RuleFor(c => c.Ano).NotEmpty();

        RuleFor(c => c.Ano).GreaterThanOrEqualTo(1885);

        RuleFor(c => c.Modelo).NotEmpty();

        RuleFor(c => c.Placa).NotEmpty();
    }
}
