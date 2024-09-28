using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Rentals.ReserveRental;

public sealed record ReserveRentalCommand(
    Guid UserId,
    Guid MotoId,
    DateTime StartDate,
    DateTime EndDate,
    DateTime PredictedEndDate,
    int Plan) : ICommand<Guid>;
