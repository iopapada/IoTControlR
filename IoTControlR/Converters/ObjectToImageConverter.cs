using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace IoTControlR.Converters
{
    public class ObjectToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ImageSource imageSource)
            {
                return imageSource;
            }
            if (value is String url)
            {
                return url;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
