using BcWpfCommon.Commands;
using BOSSAutoRef.Factory;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CustomerControls
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private CustomerModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CustomerId
        {
            get => model?.CustomerId.ToString();
            set
            {
                if (value == model.CustomerId.ToString())
                    return;
                if (value == string.Empty)
                {
                    model.CustomerId = 0;
                }
                else if (int.TryParse(value, out int customerId))
                {
                    model.CustomerId = customerId;
                }
                OnPropertyChanged("CustomerId");
            }
        }

        public string Name
        {
            get => model?.CustomerName;
            set
            {
                if (value == model.CustomerName)
                    return;
                model.CustomerName = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get => model?.Description;
            set
            {
                if (value.Length > 50)
                {
                    throw new Exception("Description longer than 10 characters not allowed.");
                }
                model.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public string ClearanceType
        {
            get => model.ClearanceType;
            set
            {
                if (value == null)
                {
                    model.ClearanceType = null;
                    return;
                }
                var clearanceType = value.Split(' ').FirstOrDefault();
                if (clearanceType == "PGS")
                {
                    throw new Exception("Personal Goods are not allowed");
                }
                model.ClearanceType = clearanceType;
                OnPropertyChanged("ClearanceType");
            }
        }

        public string NormalClearanceType
        {
            get => model.NormalClearanceType;
            set
            {
                if (value == null)
                {
                    model.NormalClearanceType = null;
                    return;
                }
                model.NormalClearanceType = value;
                OnPropertyChanged("NormalClearanceType");
            }
        }

        public string NewClearanceType
        {
            get => model.NewClearanceType;
            set
            {
                if (value == null)
                {
                    model.NewClearanceType = null;
                    return;
                }
                if (value == "FML")
                {
                    throw new Exception("Formal Entry is not allowed");
                }
                model.NewClearanceType = value;
                OnPropertyChanged("NewClearanceType");
            }
        }

        public DataView ClearanceTypes => GetClearanceTypes();


        public string WeightUnit
        {
            get { return model?.WeightUnit; }
            set 
            { 
                model.WeightUnit = value;
                OnPropertyChanged("WeightUnit");
                OnPropertyChanged("WeightObject");
            }
        }

        public double Weight
        {
            get
            {
                if (model == null)
                    return 0;

                return model.Weight;
            }
            set
            {
                if (!string.IsNullOrEmpty(WeightUnit))
                    model.Weight = value;
                OnPropertyChanged(nameof(Weight));
                OnPropertyChanged(nameof(WeightObject));
            }
        }

        public string WeightObject
        {
            get
            {
                return model?.Weight
                        + " "
                        + model?.WeightUnit;
            }
        }

        public string Tariff
        {
            get
            {
                return model.Tariff;
            }
            set
            {
                model.Tariff = value;
                OnPropertyChanged(nameof(Tariff));
                OnPropertyChanged(nameof(TariffDetailsWatermark));
            }
        }

        public DataView Tariffs => model?.Tariffs;

        public string TariffDetailsWatermark => GetTariffDetailsWatermark();

        private string GetTariffDetailsWatermark()
        {
            return   
                Tariffs
                    .ToTable()
                    .AsEnumerable()
                    .Where(row => row.Field<string>("HSCODE") == Tariff)
                    .Select(r => r.Field<string>("TRF_NR_TSL_TE1"))
                    .FirstOrDefault();
        }

        public CustomerViewModel()
        {
            var d = 100.0;
            model = new CustomerModel
            {
                CustomerId = 1,
                CustomerName = "Jansen",
                Description = "Onze eerste klant",
                ClearanceType = null,
                WeightUnit = "KG",
                Weight = d
            };
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ICommand LostFocusCommand
            => new ActionCommand<object>(
                    OnLostFocusExecute,
                    param => true);

        private void OnLostFocusExecute(object obj)
        {
            if (obj == null)
                return;
            var parameters = (string[])obj;
            var name = parameters[0];
            switch (name)
            {
                case "Name":
                    Name = parameters[1];
                    break;
            }
        }

        private DataView GetClearanceTypes()
        {
            var result = Builder.GetInstance().GetRefDataView(AutoRefTypes.TYPE_BrokStatusValue, "", "CT", "", false);
            if (!result.Table.Columns.Contains("CustomDescription"))
            {
                result.Table.Columns.Add("CustomDescription", typeof(string));
            }
            foreach (DataRow row in result.Table.Rows)
            {
                FillCustomDescriptionField(row);
            }
            return result;
        }

        private void FillCustomDescriptionField(DataRow row)
        {
            var builder = new StringBuilder();
            builder.Append(row["Code"]);
            builder.Append(" ");
            builder.Append(row["Description"]);

            if (row.Table.Columns.Contains("OriginalDescription"))
            {
                if (!row["Description"].ToString().Equals(row["OriginalDescription"].ToString()))
                {
                    builder.Append(" ");
                    builder.Append(row["OriginalDescription"]);
                }
            }
            row["CustomDescription"] = builder.ToString();
        }

    }
}
