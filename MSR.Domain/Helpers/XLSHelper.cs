using ExcelDataReader;
using System.Data;
using System.IO;

namespace MSR.Domain.Helpers
{
    public class XLSHelper
    {
        public static DataSet ParseRecords(byte[] data)
        {
            var stream = new MemoryStream(data);
            var reader = ExcelReaderFactory.CreateReader(stream);
            return reader.AsDataSet();
        }
    }
}
