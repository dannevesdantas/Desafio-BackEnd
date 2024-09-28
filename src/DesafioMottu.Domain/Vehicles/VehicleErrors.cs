using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Vehicles;

public static class VehicleErrors
{
    public static readonly Error NotFound = new(
        "Vehicle.NotFound",
        "The vehicle with the specified identifier was not found");

    public static readonly Error AlreadyExists = new(
        "Vehicle.AlreadyExists",
        "The vehicle already exists");

    public static readonly Error PlateAlreadyTaken = new(
        "Vehicle.PlateAlreadyTaken",
        "There is a vehicle with this license plate");

    public static readonly Error DeleteAlreadyRented = new(
        "Vehicle.DeleteAlreadyRented",
        "The vehicle was already rented and cannot be deleted");
}
