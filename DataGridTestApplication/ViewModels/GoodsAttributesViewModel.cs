using BcWpfCommon.Commands;
using DataGridTestApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DataGridTestApplication.ViewModels
{
    public class GoodsAttributesViewModel : INotifyPropertyChanged
    {
        private enum Names { First, Second, Third, Fourth, Fifth, Sixth, Seventh, Eight, Ninth, Tenth}
        private const int ElementsPerRow = 3;

        public GoodsAttributesViewModel()
        {
            GoodsAttributes = new ObservableCollection<GoodsAttribute>();
            GoodsAttributeRows = new ObservableCollection<GoodsAttribute[]>();
            LoadGoodsAttributes();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GoodsAttribute> GoodsAttributes
        {
            get; private set;
        }

        public ObservableCollection<GoodsAttribute[]> GoodsAttributeRows { get; private set; }

        public ICommand ConfirmCommand => new ActionCommand<object>(ConfirmCommandExecute);

        private void ConfirmCommandExecute(object obj)
        {
            var selectedAttributes = GoodsAttributes.Where(a => a.IsSelected).Select(a => a.Name).ToArray();
            var stringBuilder = new StringBuilder();
            foreach (var attribute in selectedAttributes)
            {
                stringBuilder.AppendLine(attribute);
            }
            MessageBox.Show(stringBuilder.ToString(), "Selected attributes");
            (obj as Window)?.Close();
        }

        private void LoadGoodsAttributes()
        {
            var goodsAttributesRow = new List<GoodsAttribute>();
            for (var i = 0; i < 10; i++)
            {
                var type = typeof(Names);
                var name = Enum.GetValues(type).GetValue(i).ToString();
                var attribute = new GoodsAttribute
                {
                    Name = name
                };
                attribute.PropertyChanged += OnAttributePropertyChanged;
                GoodsAttributes.Add(attribute);
                goodsAttributesRow.Add(attribute);
                if ((i + 1) % ElementsPerRow != 0)
                {
                    continue;
                }
                GoodsAttributeRows.Add(goodsAttributesRow.ToArray());
                goodsAttributesRow.Clear();
            }
            if (goodsAttributesRow.Count > 0)
            {
                GoodsAttributeRows.Add(goodsAttributesRow.ToArray());
            }
        }

        private GoodsAttribute selectedAttribute;
        public GoodsAttribute SelectedAttribute
        {
            get => selectedAttribute;
            set
            {
                selectedAttribute = value;
                OnPropertyChanged(nameof(SelectedAttribute));
            }
        }

        private bool areAllSelected;
        public bool AreAllSelected
        {
            get => areAllSelected;
            set
            {
                areAllSelected = value;
                foreach (var attribute in GoodsAttributes)
                {
                    attribute.PropertyChanged -= OnAttributePropertyChanged;
                    attribute.IsSelected = areAllSelected;
                    attribute.PropertyChanged += OnAttributePropertyChanged;
                }
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnAttributePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            areAllSelected = GoodsAttributes.All(c => c.IsSelected);
            OnPropertyChanged(null);
        }
    }
}