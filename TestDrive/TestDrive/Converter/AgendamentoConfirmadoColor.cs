using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestDrive.Converter
{
    public class AgendamentoConfirmadoColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var confirmado = (bool)value;

            if (confirmado)
                return Color.Black;
            else
                return Color.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
