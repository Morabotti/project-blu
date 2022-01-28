using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjectBlu.Repositories.Converters;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(d => d.ToDateTime(TimeOnly.MinValue), d => DateOnly.FromDateTime(d))
    {
    }
}

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base((d1, d2) => d1 == d2 && d1.DayNumber == d2.DayNumber, d => d.GetHashCode())
    {
    }
}