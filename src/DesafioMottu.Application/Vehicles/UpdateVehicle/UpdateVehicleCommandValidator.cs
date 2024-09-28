using FluentValidation;

namespace DesafioMottu.Application.Motorcycles.UpdateVehicle;

internal class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleCommandValidator()
    {
        RuleFor(c => c.LicensePlateNumber).NotEmpty();
    }
}
