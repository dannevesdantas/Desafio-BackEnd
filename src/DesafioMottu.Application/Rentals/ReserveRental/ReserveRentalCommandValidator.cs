using FluentValidation;

namespace DesafioMottu.Application.Rentals.ReserveRental;

internal class ReserveRentalCommandValidator : AbstractValidator<ReserveRentalCommand>
{
    public ReserveRentalCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.MotoId).NotEmpty();

        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(c => c.EndDate).NotEmpty();

        RuleFor(c => c.PredictedEndDate).NotEmpty();

        RuleFor(c => c.StartDate.Date).Equal(DateTime.Now.AddDays(1).Date);

        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}
