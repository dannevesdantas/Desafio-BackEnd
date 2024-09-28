using DesafioMottu.Application.Abstractions.Messaging;
using DesafioMottu.Application.Abstractions.Storage;
using DesafioMottu.Domain.Abstractions;
using DesafioMottu.Domain.DriversLicense;
using DesafioMottu.Domain.Users;

namespace DesafioMottu.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IDriversLicenseRepository _driversLicenseRepository;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
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
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        DriversLicense? driversLicense = await _driversLicenseRepository.GetByUserIdAsync(user.Id, cancellationToken);

        if (driversLicense is null)
        {
            return Result.Failure<Guid>(UserErrors.Unlicensed);
        }

        _storageService.SaveImageAsync(request.DriversLicenseImage, request.UserId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
