using System.Globalization;

namespace Bijington.Cupcakes.Converters;

public class DueStatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            var remainingDays = (dateTime - DateTime.Today).Days;

            return remainingDays switch
            {
                < 0 => "Overdue",
                0 => "Today",
                1 => "Tomorrow",
                _ => $"{remainingDays} days"
            };
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}