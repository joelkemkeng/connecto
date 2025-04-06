using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace connecto.crossplat.Converters
{
    public class BoolToWidthConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isExpanded)
            {
                return isExpanded ? 220.0 : 70.0;
            }
            return 70.0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 