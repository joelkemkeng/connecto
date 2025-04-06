using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Platform;
using Avalonia.Media.Imaging;

namespace connecto.crossplat.Converters
{
    public class AssetPathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                try
                {
                    // Normaliser le chemin
                    if (path.StartsWith("/"))
                        path = path.Substring(1);

                    // Charger l'asset via l'AssetLoader
                    var assets = AssetLoader.Open(new Uri($"avares://connecto.crossplat/{path}"));
                    if (assets != null)
                    {
                        return new Bitmap(assets);
                    }
                }
                catch (Exception)
                {
                    // En cas d'erreur, retourner null
                    return null;
                }
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 