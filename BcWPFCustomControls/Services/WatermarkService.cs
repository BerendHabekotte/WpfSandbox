using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using BcWPFCustomControls.Controls;

namespace BcWPFCustomControls.Services
{
    /// <summary>
    /// Class that provides the Watermark attached property
    /// https://stackoverflow.com/questions/833943/watermark-hint-placeholder-text-in-textbox
    /// </summary>
    public static class WatermarkService
    {
        /// <summary>
        /// Watermark Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
           "Watermark",
           typeof(object),
           typeof(WatermarkService),
           new FrameworkPropertyMetadata(null, OnWatermarkChanged));

        #region Private Fields

        /// <summary>
        /// Dictionary of ItemsControls
        /// </summary>
        private static readonly Dictionary<object, ItemsControl> ItemsControls = new Dictionary<object, ItemsControl>();

        #endregion

        /// <summary>
        /// Gets the Watermark property.  This dependency property indicates the watermark for the control.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to get the property from</param>
        /// <returns>The value of the Watermark property</returns>
        public static object GetWatermark(DependencyObject d)
        {
            return d.GetValue(WatermarkProperty);
        }

        /// <summary>
        /// Sets the Watermark property.  This dependency property indicates the watermark for the control.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> to set the property on</param>
        /// <param name="value">value of the property</param>
        public static void SetWatermark(DependencyObject d, object value)
        {
            d.SetValue(WatermarkProperty, value);
        }

        /// <summary>
        /// Handles changes to the Watermark property.
        /// </summary>
        /// <param name="d"><see cref="DependencyObject"/> that fired the event</param>
        /// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;
            control.Loaded += Control_Loaded;

            if (d is ComboBox)
            {
                control.GotKeyboardFocus += Control_GotKeyboardFocus;
                control.LostKeyboardFocus += Control_GotKeyboardFocus;
                ((ComboBox)control).SelectionChanged += Control_GotKeyboardFocus;
                ((ComboBox)control).KeyUp += WatermarkService_KeyUp;
            }
            else if (d is TextBox)
            {
                control.GotKeyboardFocus += Control_GotKeyboardFocus;
                control.LostKeyboardFocus += Control_Loaded;
                ((TextBox)control).TextChanged += Control_GotKeyboardFocus;
            }
            else if (d is ScrollViewer)
            {
                control.GotKeyboardFocus += Control_GotKeyboardFocus;
                control.LostKeyboardFocus += Control_Loaded;
                var textBox = GetParentTextBox(control);
                if (textBox != null)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                    if (!(textBox is BossTextBox))
                    {
                        return;
                    }
                    ((BossTextBox)textBox).PropertyChanged += WatermarkService_PropertyChanged;
                }
            }
            else if (d is ItemsControl itemsControl)
            {
                ItemsControl i = itemsControl;

                // for Items property  
                i.ItemContainerGenerator.ItemsChanged += ItemsChanged;
                ItemsControls.Add(i.ItemContainerGenerator, i);

                // for ItemsSource property  
                DependencyPropertyDescriptor prop = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, i.GetType());
                prop.AddValueChanged(i, ItemsSourceChanged);
            }
        }

        private static void WatermarkService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is TextBox textBox))
            {
                return;
            }
            Control c = textBox;
            var scrollViewer = GetChildScrollViewer(c);
            if (ShouldShowWatermark(scrollViewer))
            {
                ShowWatermark(scrollViewer);
            }
            else
            {
                RemoveWatermark(scrollViewer);
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Control c = (Control)sender;
            var scrollViewer = GetChildScrollViewer(c);
            if (ShouldShowWatermark(scrollViewer))
            {
                ShowWatermark(scrollViewer);
            }
            else
            {
                RemoveWatermark(scrollViewer);
            }
        }

        private static void WatermarkService_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Source is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    ShowWatermark((Control)sender);
                }
                else
                {
                    RemoveWatermark((Control)sender);
                }
            }
        }

        #region Event Handlers

        /// <summary>
        /// Handle the GotFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void Control_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            Control c = (Control)sender;
            if (ShouldShowWatermark(c))
            {
                ShowWatermark(c);
            }
            else
            {
                RemoveWatermark(c);
            }
        }

        /// <summary>
        /// Handle the Loaded and LostFocus event on the control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
        private static void Control_Loaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowWatermark(control))
            {
                ShowWatermark(control);
            }
        }

        /// <summary>
        /// Event handler for the items source changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="EventArgs"/> that contains the event data.</param>
        private static void ItemsSourceChanged(object sender, EventArgs e)
        {
            ItemsControl c = (ItemsControl)sender;
            if (c.ItemsSource != null)
            {
                if (ShouldShowWatermark(c))
                {
                    ShowWatermark(c);
                }
                else
                {
                    RemoveWatermark(c);
                }
            }
            else
            {
                ShowWatermark(c);
            }
        }

        /// <summary>
        /// Event handler for the items changed event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="ItemsChangedEventArgs"/> that contains the event data.</param>
        private static void ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (ItemsControls.TryGetValue(sender, out ItemsControl control))
            {
                if (ShouldShowWatermark(control))
                {
                    ShowWatermark(control);
                }
                else
                {
                    RemoveWatermark(control);
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Remove the watermark from the specified element
        /// </summary>
        /// <param name="control">Element to remove the watermark from</param>
        private static void RemoveWatermark(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null)
            {
                Adorner[] adorners = layer.GetAdorners(control);
                if (adorners == null)
                {
                    return;
                }

                foreach (Adorner adorner in adorners)
                {
                    if (adorner is WatermarkAdorner)
                    {
                        adorner.Visibility = Visibility.Hidden;
                        layer.Remove(adorner);
                    }
                }
            }
        }

        /// <summary>
        /// Show the watermark on the specified control
        /// </summary>
        /// <param name="control">Control to show the watermark on</param>
        private static void ShowWatermark(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);

            // layer could be null if control is no longer in the visual tree
            if (layer != null)
            {
                layer.Add(new WatermarkAdorner(control, GetWatermark(control)));
            }
        }

        /// <summary>
        /// Indicates whether or not the watermark should be shown on the specified control
        /// </summary>
        /// <param name="c"><see cref="Control"/> to test</param>
        /// <returns>true if the watermark should be shown; false otherwise</returns>
        private static bool ShouldShowWatermark(Control c)
        {
            if (c is ComboBox comboBox)
            {
                return comboBox.Text == string.Empty;
            }
            else if (c is TextBox textBox)
            {
                return textBox.Text == string.Empty;
            }
            else if (c is ItemsControl itemsControl)
            {
                return itemsControl.Items.Count == 0;
            }
            else if (c is ScrollViewer)
            {
                return IsTextBoxParentTextEmpty(c);
            }
            else
            {
                return false;
            }
        }

        private static TextBox GetParentTextBox(DependencyObject dependencyObject)
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null)
            {
                return null;
            }
            if (parent is TextBox)
            {
                return (parent as TextBox);
            }
            if (parent is Grid || parent is UserControl || parent is Page || parent is Window)
            {
                return null;
            }
            return GetParentTextBox(parent);
        }

        private static bool IsTextBoxParentTextEmpty(DependencyObject dependencyObject)
        {
            var textBox = GetParentTextBox(dependencyObject);
            if (textBox == null)
            {
                return false;
            }
            return textBox.Text == string.Empty;
        }

        private static ScrollViewer GetChildScrollViewer(DependencyObject dependencyObject)
        {
            ScrollViewer result = null;
            var count = VisualTreeHelper.GetChildrenCount(dependencyObject);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);
                if (child is ScrollViewer viewer)
                { 
                    result = viewer;
                }
                else
                {
                    result = GetChildScrollViewer(child);
                }
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        #endregion
    }
}
