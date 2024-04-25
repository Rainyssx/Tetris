using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        private readonly GameViewModel _viewModel;  //создание модели представления
        public GameWindow() //конструктор класса
        {
            _viewModel = new(new User());
            InputBindings.Add(new KeyBinding(_viewModel.StartCommand, Key.Space, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(_viewModel.RotateCommand, Key.Up, ModifierKeys.None) { CommandParameter = Directions.ToTopOnClockRotation });
            InputBindings.Add(new KeyBinding(_viewModel.RotateCommand, Key.Down, ModifierKeys.None) { CommandParameter = Directions.ToDownOffClockRotation });
            InputBindings.Add(new KeyBinding(_viewModel.RotateCommand, Key.Left, ModifierKeys.None) { CommandParameter = Directions.ToLeft });
            InputBindings.Add(new KeyBinding(_viewModel.RotateCommand, Key.Right, ModifierKeys.None) { CommandParameter = Directions.ToRight });
            InitializeComponent();
            DataContext = _viewModel;
            GameElems.ItemsSource = _viewModel.Field;
        }

        

        private void GameElems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _viewModel.Field.Width = (int)e.NewSize.Width;
            _viewModel.Field.Height = (int)e.NewSize.Height;
        } //Изменение размера окна


















    }
}