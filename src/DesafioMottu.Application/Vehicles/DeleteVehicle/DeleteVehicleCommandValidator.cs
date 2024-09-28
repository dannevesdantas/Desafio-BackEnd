using FluentValidation;

namespace DesafioMottu.Application.Motorcycles.DeleteVehicle;

internal class DeleteVehicleCommandValidator : AbstractValidator<DeleteVehicleCommand>
{
    public DeleteVehicleCommandValidator()
    {
        RuleFor(c => c.MotoId).NotEmpty();
    }
}
