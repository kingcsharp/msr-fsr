using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteModel>> GetQuotesAsync();

        /// <summary>
        /// Returns a Quote based on <paramref name="id"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<QuoteModel>> GetQuoteAsync(int id);

        /// <summary>
        /// Create a Quote using <paramref name="command"/>
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<QuoteModel> CreateQuoteAsync(CreateQuote command);

        /// <summary>
        /// Deletes a Quote by ID in <paramref name="command"/>.Id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<QuoteModel> DeleteQuoteAsync(DeleteQuote command);
    }
}
