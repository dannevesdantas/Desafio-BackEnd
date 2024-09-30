using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Rentals.RentVehicle;

public sealed record RentVehicleCommand(
    Guid UserId,
    Guid VehicleId,
    DateTime StartDate,
    DateTime EndDate,
    DateTime PredictedEndDate,
    int Plan) : ICommand<Guid>;
