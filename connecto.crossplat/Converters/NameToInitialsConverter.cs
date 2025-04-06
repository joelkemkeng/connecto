using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace connecto.crossplat.Converters
{
    public class NameToInitialsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string name && !string.IsNullOrEmpty(name))
            {
                var words = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 0)
                {
                    // Si le nom contient plusieurs mots, prendre les initiales des deux premiers mots
                    if (words.Length > 1)
                    {
                        return $"{words[0][0]}{words[1][0]}".ToUpper();
                    }
                    // Si le nom contient un seul mot, prendre les deux premières lettres
                    return name.Length > 1 ? name.Substring(0, 2).ToUpper() : name[0].ToString().ToUpper();
                }
            }
            return "#";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 