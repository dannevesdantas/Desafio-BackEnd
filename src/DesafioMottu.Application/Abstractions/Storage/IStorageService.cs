using Aspose.Drawing;

namespace DesafioMottu.Application.Abstractions.Storage;

public interface IStorageService
{
    Task SaveImageAsync(Image image, Guid id);
}
