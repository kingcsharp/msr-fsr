using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MSR.Domain.Helpers
{
    public static class XLSHelper
    {
        public static DataSet ParseRecords(byte[] data)
        {
            var stream = new MemoryStream(data);
            var reader = ExcelReaderFactory.CreateReader(stream);
            return reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
        }

        public static List<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();
            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                if(!row.Table.Columns.Contains(property.Name))
                {
                    continue;
                }
                if (property.PropertyType == typeof(DayOfWeek))
                {
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), row[property.Name].ToString());
                    property.SetValue(item, day, null);
                }
                else
                {
                    if (row[property.Name] == DBNull.Value)
                    {
                        property.SetValue(item, null, null);
                    }
                    else
                    {
                        if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                        {
                            //nullable
                            object convertedValue = null;
                            try
                            {
                                convertedValue = Convert.ChangeType(row[property.Name], Nullable.GetUnderlyingType(property.PropertyType));
                            }
                            catch (Exception)
                            {
                            }
                            property.SetValue(item, convertedValue, null);
                        }
                        else if(property.PropertyType == typeof(int) && row[property.Name].GetType() == typeof(double))
                        {
                            property.SetValue(item, Convert.ToInt32(row[property.Name]), null);
                        }
                        else
                        {
                            property.SetValue(item, row[property.Name], null);
                        }
                    }
                }
            }
            return item;
        }
    }
}
