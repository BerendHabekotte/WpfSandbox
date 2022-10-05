using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossTextBox : TextBox
    {
        static BossTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BossTextBox),
                new FrameworkPropertyMetadata(typeof(BossTextBox)));
        }

        public BossTextBox()
        {
            KeyDown += BossTextBox_KeyDown;
        }

        public bool IsEnterKeyAsTab
        {
            get => (bool)GetValue(IsEnterKeyAsTabProperty);
            set => SetValue(IsEnterKeyAsTabProperty, value);
        }

        public static readonly DependencyProperty IsEnterKeyAsTabProperty =
            DependencyProperty.Register(
                "IsEnterKeyAsTab",
                typeof(bool),
                typeof(BossTextBox),
                new PropertyMetadata(false));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(BossTextBox),
                new PropertyMetadata(new PropertyChangedCallback(PlaceholderTextChanged)));

        private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public HorizontalAlignment WatermarkHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(WatermarkHorizontalAlignmentProperty); }
            set { SetValue(WatermarkHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty WatermarkHorizontalAlignmentProperty =
            DependencyProperty.Register(
                "WatermarkHorizontalAlignment",
                typeof(HorizontalAlignment),
                typeof(BossTextBox),
                new PropertyMetadata(new PropertyChangedCallback(WatermarkHorizontalAlignmentChanged)));

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void BossTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key == Key.Enter))
            {
                return;
            }
            if (!IsEnterKeyAsTab)
            {
                return;
            }
            e.Handled = true;
            System.Windows.Forms.SendKeys.Send("{Tab}");
        }
    }
}
