using System.Numerics;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Rentals.Events;
using DesafioMottu.Domain.Shared;
using MediatR;

namespace DesafioMottu.Domain.Rentals;

public sealed class Rental : Entity
{
    private DateTime _predictedEndDate;

    private Rental(Guid id,
        Guid userId,
        Guid vehicleId,
        DateRange duration,
        DateTime predictedEndDate,
        Plan plan,
        Money totalPrice,
        DateTime createdOnUtc)
        : base(id)
    {
        UserId = userId;
        VehicleId = vehicleId;
        Duration = duration;
        PredictedEndDate = predictedEndDate;
        Plan = plan;
        TotalPrice = totalPrice;
        CreatedOnUtc = createdOnUtc;
    }

    private Rental()
    {
    }

    public Guid UserId { get; private set; }

    public Guid VehicleId { get; private set; }

    public DateRange Duration { get; private set; }

    public DateTime PredictedEndDate
    {
        get => _predictedEndDate;
        private set => _predictedEndDate = value.Date.AddDays(1).AddTicks(-1);
    }

    public DateTime? ReturnedOnUtc { get; private set; }

    public Plan Plan { get; private set; }

    public Money? TotalPrice { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Result<Rental> Reserve(Guid userId,
        DriversLicense.DriversLicense userDriversLicense,
        Vehicles.Vehicle vehicle,
        DateRange duration,
        DateTime predictedEndDate,
        Plan plan,
        DateTime utcNow)
    {
        Result<DriversLicense.DriversLicense> legalRequirementsResult = VerifyLicenseLegalRequirements(userDriversLicense);

        if (legalRequirementsResult.IsFailure)
        {
            return Result.Failure<Rental>(legalRequirementsResult.Error);
        }

        Result endDateVerificationResult = VerifyEndDateMeetPlan(duration, plan);

        if (endDateVerificationResult.IsFailure)
        {
            return Result.Failure<Rental>(endDateVerificationResult.Error);
        }

        var rental = new Rental(
            Guid.NewGuid(),
            userId,
            vehicle.Id,
            duration,
            predictedEndDate,
            plan,
            null,
            utcNow);

        vehicle.LastRentedOnUtc = utcNow;

        rental.RaiseDomainEvent(new VehicleRentedDomainEvent(rental.Id));

        return rental;
    }

    private static Result VerifyEndDateMeetPlan(DateRange duration, Plan plan)
    {
        DateOnly providedEndDate = DateOnly.FromDateTime(duration.End);
        DateOnly calculatedEndDate = DateOnly.FromDateTime(duration.Start.AddDays(plan.Days)).AddDays(-1);

        if (providedEndDate != calculatedEndDate)
        {
            return Result.Failure<Rental>(RentalErrors.InvalidEndDate);
        }

        return Result.Success();
    }

    private static Result<DriversLicense.DriversLicense> VerifyLicenseLegalRequirements(DriversLicense.DriversLicense driversLicense)
    {
        if (!driversLicense.Types.Contains('A'))
        {
            return Result.Failure<DriversLicense.DriversLicense>(RentalErrors.DiversLicenseNotAllowed);
        }

        return Result.Success(driversLicense);
    }

    public Result Return(DateTime utcNow, PricingService pricingService)
    {
        if (utcNow < Duration.Start)
        {
            return Result.Failure<Guid>(RentalErrors.TooEarly);
        }

        if (ReturnedOnUtc is not null)
        {
            return Result.Failure<Guid>(RentalErrors.AlreadyReturned);
        }

        ReturnedOnUtc = utcNow;

        PricingDetails pricingDetails = pricingService.CalculatePrice(Duration, PredictedEndDate, ReturnedOnUtc.Value, Plan);

        TotalPrice = pricingDetails.TotalPrice;

        RaiseDomainEvent(new VehicleReturnedDomainEvent(Id));

        return Result.Success();
    }
}
