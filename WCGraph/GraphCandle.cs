
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace SoloVova.Dev.WCGraph{

    public class GraphCandle : FrameworkElement{
        public static DependencyProperty BackgroundColorProperty;
        static GraphCandle()
        {
            FrameworkPropertyMetadata metadata =
                new FrameworkPropertyMetadata(Colors.Yellow);
            metadata.AffectsRender = true;
            BackgroundColorProperty = DependencyProperty.Register("BackgroundColor", 
                typeof(Color), typeof(GraphCandle), metadata);
        }

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.InvalidateVisual();
        }
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Rect bounds = new Rect(0, 0, base.ActualWidth, base.ActualHeight);
            drawingContext.DrawRectangle(GetForegroundBrush(), null, bounds);
        }

        private Brush GetForegroundBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }
            else
            {
                RadialGradientBrush brush = new RadialGradientBrush(Colors.White, BackgroundColor);
                Point pt = Mouse.GetPosition(this);
                Point pt1 = new Point(pt.X / base.ActualWidth, pt.Y / base.ActualHeight);
                brush.GradientOrigin = pt1;
                brush.Center = pt1;
                return brush;
            }
        }

    }
}