using Aspose.Drawing;
using DesafioMottu.Application.Abstractions.Messaging;

namespace DesafioMottu.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, Image DriversLicenseImage) : ICommand<Guid>;
