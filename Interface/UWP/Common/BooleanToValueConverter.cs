using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BGPCastUWP.Interface.UWP.Common
{
    class BooleanToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) {
            String[] parameters = (parameter as string ?? string.Empty).Split('|');
            return (bool)value ? parameters.First() : parameters.Last();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) { 
            throw new NotImplementedException();
        }
    }
}
