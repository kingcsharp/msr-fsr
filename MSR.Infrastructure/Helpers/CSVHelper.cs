using CsvHelper;
using System.Collections;
using System.Globalization;
using System.IO;

namespace MSR.Infrastructure.Helpers
{
    public class CSVHelper
    {
        public static IEnumerable ParseRecords<T>(string data)
        {
            var reader = new StringReader(data);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }
    }
}
