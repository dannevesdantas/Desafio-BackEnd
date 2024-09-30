using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Vehicles.RegisterVehicle;

public sealed record RegisterVehicleCommand(int Ano, string Modelo, string Placa) : ICommand<Guid>;
