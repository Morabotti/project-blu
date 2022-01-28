using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjectBlu.Repositories.Converters;

public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
{
    public NullableDateOnlyConverter() : base(
        d => d == null
            ? null
            : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
        d => d == null
            ? null
            : new DateOnly?(DateOnly.FromDateTime(d.Value)))
    {
    }
}

public class NullableDateOnlyComparer : ValueComparer<DateOnly?>
{
    public NullableDateOnlyComparer() : base(
        (d1, d2) => d1 == d2 && d1.GetValueOrDefault().DayNumber == d2.GetValueOrDefault().DayNumber,
        d => d.GetHashCode())
    {
    }
}