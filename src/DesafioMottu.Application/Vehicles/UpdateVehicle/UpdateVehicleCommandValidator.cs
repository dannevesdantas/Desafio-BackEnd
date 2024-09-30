using FluentValidation;

namespace DesafioMottu.Application.Vehicles.UpdateVehicle;

internal class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(c => c.LicensePlateNumber).NotEmpty();
    }
}
