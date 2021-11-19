using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MSR.Domain.Helpers
{
    public static class CSVHelper
    {
        public static IEnumerable<T> ParseRecords<T>(string data)
        {
            var reader = new StringReader(data);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>();
        }

        public static byte[] GenerateCSV<T>(List<T> data) where T : class
        {
            if (!data.Any())
            {
                throw new ArgumentException("Cannot Generate Data on empty dataset");
            }
            using var memStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = "," });
                csvWriter.WriteHeader(data[0].GetType());
                csvWriter.NextRecord();
                csvWriter.WriteRecords<T>(data);
            }
            return memStream.ToArray();
        }
    }
}
