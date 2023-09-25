using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

public class IsHtmlLinkConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Check if the value (text) is an HTML link
        if (value is string text)
        {
            string pattern = @"<a\s+(?:[^>]*?\s+)?href=(['""])(.*?)\1";
            return System.Text.RegularExpressions.Regex.IsMatch(text, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
