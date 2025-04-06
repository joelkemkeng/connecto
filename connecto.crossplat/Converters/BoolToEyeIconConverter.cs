using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace connecto.crossplat.Converters
{
    public class BoolToEyeIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool showPassword)
            {
                return showPassword ? "👁" : "👁";
            }
            return "👁";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 