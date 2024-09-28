using DesafioMottu.Application.Abstractions.Clock;

namespace DesafioMottu.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
