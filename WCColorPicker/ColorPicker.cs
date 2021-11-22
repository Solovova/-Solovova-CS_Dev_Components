using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SoloVova.Dev.WCColorPicker{
    public class ColorPicker : Control{
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        
        public static readonly RoutedEvent ColorChangedEvent;
        
        private Color? previousColor;
        
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        private static void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            e.CanExecute = colorPicker.previousColor.HasValue;
        }
        
        private static void UndoCommand_Executed (object sender, ExecutedRoutedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            if (colorPicker.previousColor != null){
                colorPicker.Color = (Color)colorPicker.previousColor;    
            }
        }
        
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), 
                new FrameworkPropertyMetadata(typeof(ColorPicker)));
            
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker), 
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged))); 
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged",RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));
            
            CommandManager.RegisterClassCommandBinding(typeof(ColorPicker),
                new CommandBinding(ApplicationCommands.Undo,
                    UndoCommand_Executed, UndoCommand_CanExecute));
        }
        
        private static void OnColorRGBChanged(DependencyObject sender, 
            DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
        }
        
        private static void OnColorChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPicker colorPicker = (ColorPicker)sender;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;
            
            colorPicker.previousColor = (Color)e.OldValue;
            
            RoutedPropertyChangedEventArgs<Color> oArgs = new((Color)e.OldValue, (Color)e.NewValue, ColorChangedEvent);
            oArgs.Source = sender;
            colorPicker.RaiseEvent(oArgs);
            
            
        }
        
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged{
            add{ AddHandler(ColorChangedEvent, value); }
            remove{ RemoveHandler(ColorChangedEvent, value); }
        }
    }
}