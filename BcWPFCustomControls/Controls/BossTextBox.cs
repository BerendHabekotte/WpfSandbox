﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BcWPFCustomControls.Controls
{
    public class BossTextBox : TextBox, INotifyPropertyChanged
    {
        static BossTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(BossTextBox),
                new FrameworkPropertyMetadata(typeof(BossTextBox)));
            TextProperty.OverrideMetadata(
                typeof(BossTextBox),
                new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.LostFocus,
                });
        }

        public BossTextBox()
        {
            KeyDown += BossTextBox_KeyDown;
            GotFocus += BossTextBox_GotFocus;

        }

        private void BossTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }

        public bool IsEnterKeyAsTab
        {
            get => (bool)GetValue(IsEnterKeyAsTabProperty);
            set => SetValue(IsEnterKeyAsTabProperty, value);
        }

        public static readonly DependencyProperty IsEnterKeyAsTabProperty =
            DependencyProperty.Register(
                nameof(IsEnterKeyAsTab),
                typeof(bool),
                typeof(BossTextBox),
                new PropertyMetadata(false));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                nameof(PlaceholderText),
                typeof(string),
                typeof(BossTextBox),
                new PropertyMetadata(PlaceholderTextChanged));

        private static void PlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BossTextBox)?.OnPropertyChanged(nameof(PlaceholderText));
        }

        public TextWrapping PlaceholderTextWrapping
        {
            get => (TextWrapping)GetValue(PlaceholderTextWrappingProperty);
            set => SetValue(PlaceholderTextWrappingProperty, value);
        }

        public static readonly DependencyProperty PlaceholderTextWrappingProperty =
            DependencyProperty.Register(
                nameof(PlaceholderTextWrapping),
                typeof(TextWrapping),
                typeof(BossTextBox));

        public HorizontalAlignment WatermarkHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(WatermarkHorizontalAlignmentProperty);
            set => SetValue(WatermarkHorizontalAlignmentProperty, value);
        }

        public static readonly DependencyProperty WatermarkHorizontalAlignmentProperty =
            DependencyProperty.Register(
                nameof(WatermarkHorizontalAlignment),
                typeof(HorizontalAlignment),
                typeof(BossTextBox));

        private void BossTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
