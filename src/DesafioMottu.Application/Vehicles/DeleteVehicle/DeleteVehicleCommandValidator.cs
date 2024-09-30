using FluentValidation;

namespace DesafioMottu.Application.Vehicles.DeleteVehicle;

internal class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleCommandValidator()
    {
        RuleFor(c => c.VehicleId).NotEmpty();
    }
}
