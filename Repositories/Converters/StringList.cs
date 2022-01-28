using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace ProjectBlu.Repositories.Converters;

public class StringListConverter : ValueConverter<List<string>, string>
{
    private static string Serialize(List<string> list)
    {
        return JsonSerializer.Serialize(list);
    }

    private static List<string> Deserialize(string str)
    {
        return JsonSerializer.Deserialize<List<string>>(str);
    }

    public StringListConverter() : base(l => Serialize(l), s => Deserialize(s))
    {
    }
}

public class StringListComparer : ValueComparer<List<string>>
{
    public StringListComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c)
    {
    }
}