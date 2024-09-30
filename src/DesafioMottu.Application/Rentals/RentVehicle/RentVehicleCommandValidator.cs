using FluentValidation;

namespace DesafioMottu.Application.Rentals.RentVehicle;

internal class RentVehicleCommandValidator : AbstractValidator<RentVehicleCommand>
{
    public RentVehicleCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.VehicleId).NotEmpty();

        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate);

        RuleFor(c => c.PredictedEndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate);

        RuleFor(c => c.StartDate.Date).Equal(DateTime.Now.AddDays(1).Date);

        RuleFor(c => c.Plan)
            .NotEmpty()
            .GreaterThan(0);
    }
}
