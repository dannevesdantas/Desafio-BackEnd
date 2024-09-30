using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;

namespace DesafioMottu.Application.Rentals.ReturnVehicle;

internal sealed class ReturnVehicleCommandHandler : ICommandHandler<ReturnVehicleCommand, Guid>
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;

    public ReturnVehicleCommandHandler(
        IRentalRepository rentalRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService)
    {
        _rentalRepository = rentalRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
    }

    public async Task<Result<Guid>> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.GetByIdAsync(request.RentId, cancellationToken);

        if (rental is null)
        {
            return Result.Failure<Guid>(RentalErrors.NotFound);
        }

        Result result = rental.Return(request.ReturnDate, _pricingService);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return rental.Id;
    }
}
