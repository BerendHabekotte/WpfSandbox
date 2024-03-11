using BOSSAutoRef.Data;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Threading;

namespace BOSSAutoRef.Factory
{
    public class Builder : DataSet
    {
        private static Builder builder;
        private static object lockObject = RuntimeHelpers.GetObjectValue(new object());
        private const bool IsTestInstance = false;

        public static Builder GetInstance()
        {
            ObjectFlowControl.CheckForSyncLockOnValueType(lockObject);
            bool lockTaken = false;
            try
            {
                Monitor.Enter(lockObject, ref lockTaken);
                if (builder == null)
                {
                    builder = new Builder();
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(lockObject);
                }
            }

            return builder;
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
            if (!Tables.Contains(iRefType.ToString()) && IsTestInstance)
            {
                return new DataTable(iRefType.ToString()).DefaultView;
            }

            if (!Tables.Contains(iRefType.ToString()))
            {
                LoadAutoRefData(iRefType, bIsSerialized);
            }


            object obj = lockObject;
            ObjectFlowControl.CheckForSyncLockOnValueType(obj);
            bool lockTaken = false;
            try
            {
                Monitor.Enter(obj, ref lockTaken);
                DataView dataView = DefaultViewManager.CreateDataView(Tables[iRefType.ToString()]);
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

                object obj = lockObject;
                ObjectFlowControl.CheckForSyncLockOnValueType(obj);
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(obj, ref lockTaken);
                    if (Tables.Contains(iRefType.ToString()))
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
                        case AutoRefTypes.TYPE_LocalLookupCodes:
                            json = ReferenceData.GetLocalLookupCodes();
                            break;
                        case AutoRefTypes.TYPE_MeasurementUnits:
                            json = ReferenceData.GetMeasurementUnits();
                            break;
                        default:
                            json = string.Empty;
                            break;
                    }
                    oTable = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    if (oTable == null)
                    {
                        return;
                    }
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
                if (Tables.Contains(iRefType.ToString()))
                {
                    Tables.Remove(iRefType.ToString());
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
                case AutoRefTypes.TYPE_LocalLookupCodes:
                    return true;
                default: return false;
            }
        }
    }
}
