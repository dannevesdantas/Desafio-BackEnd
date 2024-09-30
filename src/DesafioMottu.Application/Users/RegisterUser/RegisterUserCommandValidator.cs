using Aspose.Drawing.Imaging;
using FluentValidation;

namespace DesafioMottu.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();

        RuleFor(c => c.Cnpj).NotEmpty().IsValidCNPJ();

        RuleFor(c => c.BirthDate)
            .NotEmpty()
            .LessThan(DateOnly.FromDateTime(DateTime.Now).AddYears(-18));

        RuleFor(c => c.DriversLicenseNumber)
            .NotEmpty()
            .Length(11)
            .Matches("^[0-9]*$");

        RuleFor(c => c.DriversLicenseClasses)
            .NotEmpty()
            .Must(types => types.TrueForAll(type => new List<char>() { 'A', 'B' }.Contains(type)));

        RuleFor(c => c.DriversLicenseImage.RawFormat)
            .Must(format => new List<ImageFormat>() { ImageFormat.Png, ImageFormat.Bmp }.Contains(format))
            .When(c => c.DriversLicenseImage is not null);

        RuleFor(c => c.DriversLicenseImage.Width).LessThan(1024)
            .When(c => c.DriversLicenseImage is not null);

        RuleFor(c => c.DriversLicenseImage.Width).LessThan(1024)
            .When(c => c.DriversLicenseImage is not null);
    }
}
