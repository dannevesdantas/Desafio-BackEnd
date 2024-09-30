using Aspose.Drawing;
using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Name,
    string Cnpj,
    DateOnly BirthDate,
    string DriversLicenseNumber,
    List<char> DriversLicenseClasses,
    Image? DriversLicenseImage) : ICommand<Guid>;
