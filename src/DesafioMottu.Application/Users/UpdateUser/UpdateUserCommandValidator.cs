using Aspose.Drawing.Imaging;
using FluentValidation;

namespace DesafioMottu.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();

        RuleFor(c => c.DriversLicenseImage).NotEmpty();

        RuleFor(c => c.DriversLicenseImage.RawFormat)
            .Must(format => new List<ImageFormat>() { ImageFormat.Png, ImageFormat.Bmp }.Contains(format))
            .When(c => c.DriversLicenseImage is not null);

        RuleFor(c => c.DriversLicenseImage.Width).LessThan(1024)
            .When(c => c.DriversLicenseImage is not null);

        RuleFor(c => c.DriversLicenseImage.Width).LessThan(1024)
            .When(c => c.DriversLicenseImage is not null);
    }
}
