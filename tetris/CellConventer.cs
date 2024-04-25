using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace tetris
{
    public class CellConverter : IValueConverter
    {   
        //Предположительно используется для связок, устанавливает цвет клетки в зависимости от типа клетки
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            new SolidColorBrush(value switch
            {
                CellType.Figure => Cell.IsActiveField ? Colors.BlueViolet : Colors.Black,
                CellType.NotFigure => Cell.IsActiveField ? Colors.Blue : Colors.Gray,
                CellType.fallenFigure => Cell.IsActiveField ? Colors.LawnGreen : Colors.Black,
                _ => throw new NotImplementedException(), //ПРограмма добавила, не факт что надо
            }); 
        
        //Неопознанный объект,как-будто отвязывает клетки
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            DependencyProperty.UnsetValue;

    }
}
