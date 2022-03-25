using HashidsNet;
using System.Reflection;

namespace ProjectBlu.Dto;

public class HashIdAction<T, U> : IMappingAction<T, U>
{
    private readonly IHashids _hashIds;

    public HashIdAction(IHashids hashIds)
    {
        _hashIds = hashIds;
    }

    public void Process(T source, U destination, ResolutionContext context)
    {
        PropertyInfo? sourceProperty = source.GetType().GetProperty("Id");

        if (sourceProperty == null)
        {
            return;
        }

        PropertyInfo? destProperty = destination.GetType().GetProperty("Id");

        if (destProperty == null)
        { 
            return;
        }

        var hashId = sourceProperty.GetValue(source, null);

        if (sourceProperty.PropertyType == typeof(string)
            && (destProperty.PropertyType == typeof(long) || destProperty.PropertyType == typeof(int)))
        {
            var rawId = _hashIds.Decode(hashId.ToString());

            if (rawId == null || rawId.Length == 0)
            {
                return;
            } 

            destProperty.SetValue(destination, rawId[0], null);
            return;
        }

        if (sourceProperty.PropertyType == typeof(long) && destProperty.PropertyType == typeof(string))
        {
            var rawId = _hashIds.EncodeLong(long.Parse(hashId.ToString()));
            destProperty.SetValue(destination, rawId, null);
            return;
        }

        if (sourceProperty.PropertyType == typeof(int) && destProperty.PropertyType == typeof(string))
        {
            var rawId = _hashIds.Encode(int.Parse(hashId.ToString()));
            destProperty.SetValue(destination, rawId, null);
            return;
        }
    }
}
