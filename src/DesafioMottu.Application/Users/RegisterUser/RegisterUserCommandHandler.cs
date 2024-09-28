using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Application.Abstractions.Storage;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Users;

namespace DesafioMottu.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDriversLicenseRepository _driversLicenseRepository;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IDriversLicenseRepository driversLicenseRepository,
        IStorageService storageService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _driversLicenseRepository = driversLicenseRepository;
        _storageService = storageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        User? existingUser = await _userRepository.GetByCnpjAsync(new Cnpj(request.Cnpj), cancellationToken);

        if (existingUser is not null)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        DriversLicense? existingDriversLicense = await _driversLicenseRepository.GetByNumberAsync(request.DriversLicenseNumber, cancellationToken);

        if (existingDriversLicense is not null)
        {
            return Result.Failure<Guid>(DriversLicenseErrors.AlreadyExists);
        }

        var newUser = User.Create(
            new Name(request.Name),
            new Cnpj(request.Cnpj),
            request.BirthDate);

        var driversLicense = DriversLicense.Create(newUser.Id,
            request.DriversLicenseNumber,
        request.DriversLicenseTypes);

        if (request.DriversLicenseImage is not null)
        {
            _storageService.SaveImageAsync(request.DriversLicenseImage, newUser.Id);
        }

        newUser.SetDriversLicense(driversLicense);

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
