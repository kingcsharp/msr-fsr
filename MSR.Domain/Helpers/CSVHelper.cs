using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MSR.Domain.Helpers
{
    public class CSVHelper
    {
        public static IEnumerable<T> ParseRecords<T>(string data)
        {
            var reader = new StringReader(data);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }
    }
}
