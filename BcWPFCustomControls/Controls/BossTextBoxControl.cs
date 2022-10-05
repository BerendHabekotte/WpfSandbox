using System.Windows;
using System.Windows.Controls;

namespace BcWPFCustomControls.Controls
{
    public class BossTextBoxControl : Control
    {
        static BossTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BossTextBoxControl),
                new FrameworkPropertyMetadata(typeof(BossTextBoxControl)));
        }

        public BossTextBoxControl()
        {
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(BossTextBoxControl),
                new PropertyMetadata(new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(BossTextBoxControl),
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
                typeof(BossTextBoxControl),
                new PropertyMetadata(new PropertyChangedCallback(WatermarkHorizontalAlignmentChanged)));

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

    }
}
