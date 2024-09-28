using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.Rentals;

namespace DesafioMottu.Application.Rentals.ReturnRental;

internal sealed class ReturnRentalCommandHandler : ICommandHandler<ReturnRentalCommand, Guid>
{
    private readonly IRentalRepository _locacaoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;

    public ReturnRentalCommandHandler(
        IRentalRepository locacaoRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService)
    {
        _locacaoRepository = locacaoRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
    }

    public async Task<Result<Guid>> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
    {
        Domain.Rentals.Rental? locacao = await _locacaoRepository.GetByIdAsync(request.LocacaoId, cancellationToken);

        if (locacao is null)
        {
            return Result.Failure<Guid>(RentalErrors.NotFound);
        }

        Result result = locacao.Return(request.ReturnDate, _pricingService);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return locacao.Id;
    }
}
