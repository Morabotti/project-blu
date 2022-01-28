using System.Reflection;

namespace ProjectBlu.Utils;

public static class UpdateMapper
{
    public static void MapValues<T, P>(T request, P update)
    {
        foreach (PropertyInfo prop in request.GetType().GetProperties())
        {
            var newValue = prop.GetValue(request);
            var currentValue = update
                    .GetType()
                    .GetProperty(prop.Name)
                    .GetValue(update);

            if (newValue != null && (newValue != currentValue))
            {
                update
                    .GetType()
                    .GetProperty(prop.Name)
                    .SetValue(update, newValue);
            }
        }
    }
}
