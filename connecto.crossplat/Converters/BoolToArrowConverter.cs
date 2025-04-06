using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace connecto.crossplat.Converters
{
    public class BoolToArrowConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isExpanded)
            {
                return isExpanded ? "◀" : "▶";
            }
            return "◀";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 