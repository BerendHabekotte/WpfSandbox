using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOSSAutoRef
{
    public class DataViewGenerator
    {
        DataSet dataSet;

        public DataViewGenerator()
        {
            dataSet = new DataSet();
        }

        public DataView CreateDataView()
        {
            DataTable table;
            string json;
            switch (iRefType)
            {
                case AutoRefTypes.TYPE_BrokStatusValue:
                    json = ReferenceData.GetBrokerageStatusValues();
                    break;
                case AutoRefTypes.TYPE_MeasurementUnits:
                    json = ReferenceData.GetMeasurementUnits();
                    break;
                default:
                    json = string.Empty;
                    break;
            }
            table = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            table.TableName = iRefType.ToString();
            Tables.Add(table);
        }

    }
}
}
