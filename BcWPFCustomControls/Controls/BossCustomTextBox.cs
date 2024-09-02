using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossCustomTextBox : TextBox
    {
        public static readonly DependencyProperty IsEnterKeyAsTabProperty =
            DependencyProperty.Register(
                "IsEnterKeyAsTab",
                typeof(bool),
                typeof(BossCustomTextBox),
                new PropertyMetadata(false));

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                "PlaceholderText",
                typeof(string),
                typeof(BossCustomTextBox),
                new PropertyMetadata(PlaceholderTextChanged));

        public static readonly DependencyProperty WatermarkHorizontalAlignmentProperty =
            DependencyProperty.Register(
                "WatermarkHorizontalAlignment",
                typeof(HorizontalAlignment),
                typeof(BossCustomTextBox),
                new PropertyMetadata(WatermarkHorizontalAlignmentChanged));

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

        public bool IsEnterKeyAsTab
        {
            get => (bool)GetValue(IsEnterKeyAsTabProperty);
            set => SetValue(IsEnterKeyAsTabProperty, value);
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public HorizontalAlignment WatermarkHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(WatermarkHorizontalAlignmentProperty); }
            set { SetValue(WatermarkHorizontalAlignmentProperty, value); }
        }

        private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void WatermarkHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void BossCustomTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                Text = updatedText;
                ValidateStatus(e);
            }

            if (e.Key == Key.Enter)
            {
                if (IsEnterKeyAsTab)
                {
                    e.Handled = true;
                    System.Windows.Forms.SendKeys.Send("{Tab}");
                }
            }

            if (e.Key != Key.Escape)
                return;
            Text = updatedText;
            var be = GetBindingExpression(TextProperty);
            if (be == null)
                return;
            be.UpdateTarget();
        }

        private void BossCustomTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ValidateStatus(e);
            CaretIndex = Text.Length;

            if (e.OriginalSource is TextBox item) item.SelectAll();
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

        private void ValidateStatus(InputEventArgs e)
        {
            Text = updatedText;
            if (!Validation.GetHasError(this))
                return;
            e.Handled = true;
            var errors = Validation.GetErrors(this);
            //BossLibrary.MainLibrary.ShowErrors(errors[0].Exception);
        }
    }
}
