using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using DesafioMottu.Application.Abstractions.Storage;

namespace DesafioMottu.Infrastructure.Storage;

internal sealed class StorageService : IStorageService
{
    public async Task SaveImageAsync(Image image, Guid id)
    {
        Directory.CreateDirectory(Path.Combine("Files", "DriversLicenses"));
        image.Save(Path.Combine("Files", "DriversLicenses", $"{id}.jpg"), ImageFormat.Jpeg);
    }
}
