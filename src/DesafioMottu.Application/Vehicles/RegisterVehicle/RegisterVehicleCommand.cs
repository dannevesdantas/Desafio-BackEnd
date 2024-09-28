using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Motorcycles.RegisterVehicle;

public sealed record RegisterVehicleCommand(int Ano, string Modelo, string Placa) : ICommand<Guid>;
