using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;



namespace PL
{
    class Convertor
    {
        internal class BatteryToColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
                (double)value switch
                {
                    < 10 => System.Windows.Media.Brushes.DarkRed,
                    < 20 => Brushes.Red,
                    < 40 => Brushes.Yellow,
                    < 60 => Brushes.GreenYellow,
                    _ => Brushes.Green
                };
            /// <summary>
            /// not implemented
            /// </summary>
            /// <param name="value"></param>
            /// <param name="targetType"></param>
            /// <param name="parameter"></param>
            /// <param name="culture"></param>
            /// <returns></returns>
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
                throw new NotImplementedException();
        }
    }
}
