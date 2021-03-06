using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SoloVova.Dev.WCColorPicker{
    [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton)),
     TemplatePart(Name = "FlipButtonAlternate", Type = typeof(ToggleButton)),
     TemplateVisualState(Name = "Normal", GroupName = "ViewStates"),
     TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
    public class FlipPanel : Control{
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), null);

        public object FrontContent{
            get{ return GetValue(FrontContentProperty); }
            set{ SetValue(FrontContentProperty, value); }
        }

        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);

        public object BackContent{
            get{ return GetValue(BackContentProperty); }
            set{ SetValue(BackContentProperty, value); }
        }

        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), null);

        public bool IsFlipped{
            get{ return (bool) GetValue(IsFlippedProperty); }
            set{
                SetValue(IsFlippedProperty, value);
                ChangeVisualState(true);
            }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);

        public CornerRadius CornerRadius{
            get{ return (CornerRadius) GetValue(CornerRadiusProperty); }
            set{ SetValue(CornerRadiusProperty, value); }
        }

        static FlipPanel(){
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel),
                new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        public override void OnApplyTemplate(){
            base.OnApplyTemplate();
            ToggleButton flipButton = base.GetTemplateChild("FlipButton") as ToggleButton;
            if (flipButton != null) flipButton.Click += flipButton_Click;

            ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
            if (flipButtonAlternate != null)
                flipButtonAlternate.Click += flipButton_Click;

            this.ChangeVisualState(false);
        }

        private void flipButton_Click(object sender, RoutedEventArgs e){
            this.IsFlipped = !this.IsFlipped;
        }

        private void ChangeVisualState(bool useTransitions){
            if (!this.IsFlipped){
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else{
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }

            // Disable flipped side to prevent tabbing to invisible buttons.            
            UIElement front = FrontContent as UIElement;
            if (front != null){
                if (IsFlipped){
                    front.Visibility = Visibility.Hidden;
                }
                else{
                    front.Visibility = Visibility.Visible;
                }
            }

            UIElement back = BackContent as UIElement;
            if (back != null){
                if (IsFlipped){
                    back.Visibility = Visibility.Visible;
                }
                else{
                    back.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}