using BOSSAutoRef.Data;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace BOSSAutoRef.Factory
{
    public class Builder : DataSet
    {
        private static Builder moInstance = null;
        private static object moLockObject = RuntimeHelpers.GetObjectValue(new object());
        private static bool mbTestInstance = false;

        public static Builder GetInstance()
        {
            object obj = moLockObject;
            ObjectFlowControl.CheckForSyncLockOnValueType(obj);
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                if (moInstance == null)
                {
                    moInstance = new Builder();
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(obj);
                }
            }

            return moInstance;
        }

        public DataView GetRefDataView(AutoRefTypes iRefType, string sByCode, string sByType, string sFilterDataBy, bool bIsSerialized)
        {
            try
            {
                if (Operators.CompareString(sByCode, "", false) != 0)
                {
                    if (Operators.CompareString(sFilterDataBy, "", false) != 0)
                    {
                        sFilterDataBy += " and ";
                    }

                    sFilterDataBy = sFilterDataBy + " Code = '" + sByCode + "'";
                }

                if (Operators.CompareString(sByType, "", false) != 0)
                {
                    if (Operators.CompareString(sFilterDataBy, "", false) != 0)
                    {
                        sFilterDataBy += " and ";
                    }

                    sFilterDataBy = sFilterDataBy + " Type = '" + sByType + "'";
                }

                return BuildAutoRefDataView(iRefType, sFilterDataBy, bIsSerialized);
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                throw ex2;
            }
        }

        private DataView BuildAutoRefDataView(AutoRefTypes iRefType, string sFilter = "", bool bIsSerialized = false)
        {
            if (!Tables.Contains(iRefType.ToString()) && mbTestInstance)
            {
                return new DataTable(iRefType.ToString()).DefaultView;
            }

            if (!Tables.Contains(iRefType.ToString()))
            {
                LoadAutoRefData(iRefType, bIsSerialized);
            }


            object obj = moLockObject;
            ObjectFlowControl.CheckForSyncLockOnValueType(obj);
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                DataView dataView = base.DefaultViewManager.CreateDataView(base.Tables[iRefType.ToString()]);
                dataView.RowFilter = sFilter;
                return dataView;
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(obj);
                }
            }
        }

        public void LoadAutoRefData(AutoRefTypes iRefType, bool bSerializeIt)
        {
            try
            {
                if (!IsValidReferenceType(iRefType))
                {
                    return;
                }

                object obj = moLockObject;
                ObjectFlowControl.CheckForSyncLockOnValueType(obj);
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    if (base.Tables.Contains(iRefType.ToString()))
                    {
                        return;
                    }
                    DataTable oTable;
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
                    oTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    oTable.TableName = iRefType.ToString();
                    Tables.Add(oTable);
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception ex2 = ex;
                if (base.Tables.Contains(iRefType.ToString()))
                {
                    base.Tables.Remove(iRefType.ToString());
                }

                throw ex2;
            }
        }

        private bool IsValidReferenceType(AutoRefTypes referenceType)
        {
            switch(referenceType)
            {
                case AutoRefTypes.TYPE_BrokStatusValue: 
                case AutoRefTypes.TYPE_MeasurementUnits:
                    return true;
                default: return false;
            }
        }
    }
}
