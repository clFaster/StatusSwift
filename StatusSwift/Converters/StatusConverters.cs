using System.Globalization;

namespace StatusSwift.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isActive && parameter is string paramType)
            if (paramType == "Active")
                return isActive ? Colors.LimeGreen : Colors.Gray;

        return Colors.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BoolToTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isActive && parameter is string paramType)
            if (paramType == "Status")
                return isActive ? "System Activity Enabled" : "System Activity Disabled";

        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class BoolToButtonColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isActive)
        {
            // Use application resources for colors
            if (isActive)
            {
                // Accent color for "Stop" button
                if (Application.Current?.Resources != null &&
                    Application.Current.Resources.TryGetValue("Tertiary", out var stopColor))
                    return stopColor;
                return Colors.Red;
            }

            // Primary color for "Start" button
            if (Application.Current?.Resources != null &&
                Application.Current.Resources.TryGetValue("Primary", out var startColor))
                return startColor;
            return Colors.Blue;
        }

        // Default color if not boolean
        if (Application.Current?.Resources != null &&
            Application.Current.Resources.TryGetValue("Primary", out var defaultColor))
            return defaultColor;
        return Colors.Blue;
    }


    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}