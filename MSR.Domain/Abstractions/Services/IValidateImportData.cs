using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Abstractions.Services
{
    public interface IValidateImportData
    {
        /// <summary>
        /// Validate the integrity of the input data for import.
        /// </summary>
        /// <description>
        /// Validate the integrity of the input data for import.
        ///
        /// This does NOT parse the data.  This function should
        /// validate that the input data is in the expected format for
        /// the importer, and that the data can be read and parsed
        /// into generic objects.  It should not know about column
        /// types, expected headers, or data types.  That logic should
        /// be contained within the service that does the import.
        ///
        /// </description>
        /// <param name="binData"></param>
        /// <param name="importErrors"></param>
        /// <returns></returns>
        bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors);
    }
}
