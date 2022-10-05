using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace BcWPFCustomControls
{
    [ContentProperty("Child")]
    [DefaultEvent("Opened")]
    [DefaultProperty("Child")]
    [Localizability(LocalizationCategory.None)]
    public class DraggablePopup : Popup
    {
        public UIElement TrueChild { get; private set; }

        public Thumb Thumb { get; private set; } = new Thumb
        {
            Width = 0,
            Height = 0,
        };

        public DraggablePopup()
        {
            MouseDown += (sender, e) =>
            {
                Thumb.RaiseEvent(e);
            };

            Thumb.DragDelta += (sender, e) =>
            {
                HorizontalOffset += e.HorizontalChange;
                VerticalOffset += e.VerticalChange;
            };

            Opened += DraggablePopup_Opened;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            TrueChild = Child;

            var surrogateChild = new StackPanel();

            RemoveLogicalChild(TrueChild);

            surrogateChild.Children.Add(Thumb);
            surrogateChild.Children.Add(TrueChild);

            AddLogicalChild(surrogateChild);
            Child = surrogateChild;
        }

        private void DraggablePopup_Opened(object sender, EventArgs e)
        {
            ResetPopupPosition();
        }

        private void ResetPopupPosition()
        {
            HorizontalOffset = 0;
            VerticalOffset = 0;
        }
    }
}