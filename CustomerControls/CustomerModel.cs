using System;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CustomerControls
{
    internal class CustomerModel
    {
        private readonly DataView tariffs;
        public CustomerModel()
        {
            Trace.AutoFlush = true;
            Trace.WriteLine($"{DateTime.Now} CustomerModel Constructor start.");
            var stopWatch = Stopwatch.StartNew();
            var json = BOSSAutoRef.Data.ReferenceData.GetTariffData();
            var table= (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            tariffs = table?.AsDataView();
            stopWatch.Stop();
            Trace.WriteLine($"{DateTime.Now} - Duration {stopWatch.Elapsed} - CustomerModel Constructor end.");
        }

        public int? CustomerId { get; set; }
        
        public string CustomerName { get; set; }

        public string Description { get; set; }

        public string ClearanceType { get; set; }

        public string NormalClearanceType { get; set; }

        public string NewClearanceType { get; set; }

        public double Weight { get; set; }

        public string WeightUnit { get; set; } 

        public string Tariff { get; set; }

        public string TariffDetails { get; set; }

        public string TariffWatermark { get; set; }

        public DataView Tariffs => tariffs;
    }
}
