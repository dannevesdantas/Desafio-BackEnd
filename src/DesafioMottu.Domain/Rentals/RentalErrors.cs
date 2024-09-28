using DesafioMottu.Domain.Abstractions;

namespace DesafioMottu.Domain.Rentals;

public static class RentalErrors
{
    public static readonly Error NotFound = new(
        "Rental.Found",
        "The rental with the specified identifier was not found");

    public static readonly Error Overlap = new(
        "Rental.Overlap",
        "The current rental is overlapping with an existing one");

    public static readonly Error DiversLicenseNotAllowed = new(
        "Rental.DiversLicenseNotAllowed",
        "The user diver's license doesn't meet legal requirements");
}
