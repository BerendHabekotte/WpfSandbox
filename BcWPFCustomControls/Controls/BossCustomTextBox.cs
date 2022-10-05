using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossCustomTextBox : TextBox
    {
        private string updatedText;
        static BossCustomTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BossCustomTextBox), new FrameworkPropertyMetadata(typeof(BossCustomTextBox)));
        }
        public BossCustomTextBox()
        {
            updatedText = string.Empty;
            KeyDown += BossCustomTextBox_KeyDown;
            PreviewLostKeyboardFocus += BossCustomTextBox_LostKeyboardFocus;
            TextChanged += BossCustomTextBox_TextChanged;
        }

        private void BossCustomTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                Text = updatedText;
                ValidateStatus(e);
            }

            if (e.Key != Key.Escape)
                return;
            Text = updatedText;
            BindingExpression be = GetBindingExpression(TextProperty);
            if (be == null)
                return;
            be.UpdateTarget();
        }

        private void ValidateStatus(InputEventArgs e)
        {
            Text = updatedText;
            if (!Validation.GetHasError(this))
                return;
            e.Handled = true;
        }

        private void BossCustomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!(e.OriginalSource is TextBox updatedTextBox))
                    return;
                updatedText = updatedTextBox.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BossCustomTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ValidateStatus(e);
            CaretIndex = Text.Length;
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
                typeof(BossCustomTextBox),
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
                typeof(BossCustomTextBox),
                new PropertyMetadata(new PropertyChangedCallback(WatermarkHorizontalAlignmentChanged)));

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
