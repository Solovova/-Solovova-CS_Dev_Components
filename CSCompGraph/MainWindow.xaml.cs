using System.Windows;
using System.Windows.Media;

namespace SoloVova.Dev.CSCompGraph{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        public MainWindow(){
            InitializeComponent();
        }
        
        private void graphCandle_ColorChanged0(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (Txb0 != null){
                Txb0.Text = e.NewValue.ToString();
            }
        }
        
        private void graphCandle_ColorChanged1(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (Txb1 != null){
                Txb1.Text = e.NewValue.ToString();
            }
        }
    }
}