using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace TestDrive.Converter
{
    public class UserImageFromByteArray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte[] bytes = (byte[])value;

                if(bytes.Length == 0)
                    return ImageSource.FromFile("perfil.png");


                Stream stream = new MemoryStream(bytes);

                return ImageSource.FromStream(() => stream);
            }
            else
            {
                return ImageSource.FromFile("perfil.png");
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
