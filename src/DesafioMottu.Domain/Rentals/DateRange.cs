namespace DesafioMottu.Domain.Rentals;

public sealed record DateRange
{
    private DateRange()
    {
    }

    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public int LengthInDays => (End - Start).Days;

    public static DateRange Create(DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new ApplicationException("End date precedes start date");
        }

        return new DateRange
        {
            Start = start.Date,
            End = end.Date.AddDays(1).AddTicks(-1)
        };
    }
}
